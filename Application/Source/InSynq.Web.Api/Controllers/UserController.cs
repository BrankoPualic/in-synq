using InSynq.Core.Dtos.User;
using InSynq.Core.Interfaces.Person;
using InSynq.Core.Search;
using InSynq.Web.Api.Controllers._Base;
using InSynq.Web.Api.ReinforcedTypings.Generator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InSynq.Web.Api.Controllers;

public class UserController(IUserService userService) : BaseController
{
    [HttpGet]
    [Authorize]
    [AngularMethod(typeof(UserDto))]
    public async Task<IActionResult> GetCurrentUser() => Result(await userService.GetCurrentUserAsync());

    [HttpGet("{id}")]
    [Authorize]
    [AngularMethod(typeof(UserDto))]
    public async Task<IActionResult> GetSingle(long id) => Result(await userService.GetSingleAsync(id));

    [HttpGet("{id}")]
    [Authorize]
    [AngularMethod(typeof(UserLogDto))]
    public async Task<IActionResult> GetUserLog(long id) => Result(await userService.GetUserLogAsync(id));

    [HttpGet]
    [Authorize]
    [AngularMethod(typeof(IEnumerable<UserDto>))]
    public async Task<IActionResult> Search(UserSearchOptions options) => Result(await userService.SearchAsync(options));

    [HttpPost]
    [Authorize]
    [AngularMethod(typeof(void))]
    public async Task<IActionResult> Update(UserDto data) => Result(await userService.UpdateAsync(data));
}