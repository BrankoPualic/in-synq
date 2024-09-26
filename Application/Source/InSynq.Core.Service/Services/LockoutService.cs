using InSynq.Core.Model.Models.Application.User;
using StackExchange.Redis;

namespace InSynq.Core.Service.Services;

public class LockoutService : ILockoutService
{
	private readonly IDatabase _redisDatabase;
	private readonly IUserManager _userManager;
	private const string FailedAttemptsKey = "FailedAttempts:";
	private const string LockoutKey = "Lockout:";

	public LockoutService(IConnectionMultiplexer redis, IUserManager userManager)
	{
		_redisDatabase = redis.GetDatabase();
		_userManager = userManager;
	}

	public async Task<ResponseWrapper> IsUserLockedAsync(string email)
	{
		if (await _userManager.IsUserLocked(email))
			return new(new Error(nameof(User), "Your account is locked.\r\nPlease get in touch with our Help Desk. Thank you."));

		var lockout = await _redisDatabase.StringGetAsync(LockoutKey + email);

		if (!lockout.IsNull && DateTime.TryParse(lockout, out var lockUntil))
			if (lockUntil > DateTime.UtcNow)
				return new(new Error(nameof(User), "Your account is temporarily locked.\r\nPlease try again later."));

		return new();
	}

	public async Task RegisterFailedAttemptAsync(string email)
	{
		var failedAttempts = await _redisDatabase.StringIncrementAsync(FailedAttemptsKey + email);

		switch (failedAttempts)
		{
			case 3:
				await TimeLockout(email, 5);
				break;

			case 5:
				await TimeLockout(email, 30);
				break;

			case >= 7:
				await AdminLockout(email);
				break;

			default:
				break;
		}
	}

	public async Task ResetFailedAttemptsAsync(string email)
	{
		await _redisDatabase.KeyDeleteAsync(FailedAttemptsKey + email);
		await _redisDatabase.KeyDeleteAsync(LockoutKey + email);
		await _userManager.UnlockUser(email);
	}

	// private

	private async Task TimeLockout(string email, byte minutes)
	{
		await _redisDatabase.StringSetAsync(LockoutKey + email, DateTime.UtcNow.AddMinutes(minutes).ToString());
		await _redisDatabase.KeyExpireAsync(LockoutKey + email, TimeSpan.FromMinutes(minutes));
	}

	private async Task AdminLockout(string email)
	{
		await _redisDatabase.StringSetAsync(LockoutKey + email, DateTime.MaxValue.ToString());
		await _userManager.LockUser(email);
	}
}