using InSynq.Core.Dtos.Auth;
using InSynq.Core.Interfaces;
using InSynq.Core.Model.Models.Application.User;

namespace InSynq.Core.Service.Services;

public class AuthService(IDatabaseContext context, ITokenService tokenService, IUserManager userManager, ILockoutService lockoutService) : BaseService(context), IAuthService
{
	public async Task<ResponseWrapper<TokenDto>> Signin(SigninDto data)
	{
		var model = await db.Users.GetSingleAsync(_ => _.Email == data.Email, _ => _.Roles);

		if (model.IsNullOrEmpty())
			return new(new Error(nameof(User), "Your credentials are incorrect.\r\nPlease try again."));

		// Check if the user is locked
		var result = await lockoutService.IsUserLockedAsync(data.Email);
		if (!result.IsSuccess)
			return new(result.Errors);

		// Register failed attempt if passwords do not match
		if (!userManager.VerifyPassword(data.Password, model.Password))
		{
			await lockoutService.RegisterFailedAttemptAsync(data.Email);
			return new(new Error(nameof(User), "Your credentials are incorrect.\r\nPlease try again."));
		}

		// Successful signin
		await lockoutService.ResetFailedAttemptsAsync(data.Email);

		// Signin log
		var signin = new UserSigninLog(model.Id);
		db.Create(signin);
		await db.SaveChangesAsync(false);

		var token = new TokenDto { Token = tokenService.GenerateJwtToken(model) };

		return new(token);
	}

	public Task<ResponseWrapper<TokenDto>> Signup(SignupDto data)
	{
		throw new NotImplementedException();
	}
}