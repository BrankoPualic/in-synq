namespace InSynq.Core.Interfaces;

public interface ILockoutService
{
	Task<ResponseWrapper> IsUserLockedAsync(string email);

	Task RegisterFailedAttemptAsync(string email);

	Task ResetFailedAttemptsAsync(string email);
}