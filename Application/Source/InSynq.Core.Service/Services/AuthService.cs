using InSynq.Core.Dtos.Auth;
using InSynq.Core.Interfaces;
using InSynq.Core.Model.Models.Application.User;

namespace InSynq.Core.Service.Services;

public class AuthService(IDatabaseContext context, ITokenService tokenService) : BaseService(context), IAuthService
{
	public async Task<ResponseWrapper<TokenDto>> Signin(SigninDto data)
	{
		var model = await db.Users.GetSingleAsync(_ => _.Username == data.Username || _.Email == data.Username, _ => _.Roles);

		if (model.IsNullOrEmpty())
			return new(new Error(nameof(User), "Your credentials are incorrect.\r\nPlease try again."));

		// BPR: Add signin log
		// BPR: Add incorect password lock functionality

		var token = new TokenDto
		{
			Token = tokenService.GenerateJwtToken(model)
		};

		return new(token);
	}

	public Task<ResponseWrapper<TokenDto>> Signup(SignupDto data)
	{
		throw new NotImplementedException();
	}
}