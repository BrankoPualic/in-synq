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
    [HttpGet("{id}")]
    [Authorize]
    [AngularMethod(typeof(UserDto))]
    public async Task<IActionResult> GetSingle(long id) => Result(await userService.GetSingleAsync(id));

    [HttpGet]
    [Authorize]
    [AngularMethod(typeof(IEnumerable<UserDto>))]
    public async Task<IActionResult> Search(UserSearchOptions options) => Result(await userService.SearchAsync(options));
}