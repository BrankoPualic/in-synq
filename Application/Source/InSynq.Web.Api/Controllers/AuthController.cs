using InSynq.Core.Dtos.Auth;
using InSynq.Core.Interfaces;
using InSynq.Web.Api.Controllers._Base;
using InSynq.Web.Api.ReinforcedTypings.Generator;
using Microsoft.AspNetCore.Mvc;

namespace InSynq.Web.Api.Controllers;

public class AuthController(IAuthService authService) : BaseController
{
	[HttpPost]
	[AngularMethod(typeof(TokenDto))]
	public async Task<IActionResult> Signin(SigninDto data) => Result(await authService.Signin(data));

	[HttpPost]
	[AngularMethod(typeof(TokenDto))]
	public async Task<IActionResult> Signup([FromForm] SignupDto data) => Result(await authService.Signup(data));
}