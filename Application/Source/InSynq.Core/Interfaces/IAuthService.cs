using InSynq.Core.Dtos.Auth;

namespace InSynq.Core.Interfaces;

public interface IAuthService
{
    Task<ResponseWrapper<TokenDto>> Signup(SignupDto data);

    Task<ResponseWrapper<TokenDto>> Signin(SigninDto data);
}