using InSynq.Core.Dtos.Follow;
using InSynq.Core.Interfaces.Follow;
using InSynq.Web.Api.Controllers._Base;
using InSynq.Web.Api.ReinforcedTypings.Generator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InSynq.Web.Api.Controllers;

public class FollowController(IFollowService followService) : BaseController
{
    [HttpPost]
    [Authorize]
    [AngularMethod(typeof(void))]
    public async Task<IActionResult> Follow(FollowDto data) => Result(await followService.FollowAsync(data));

    [HttpPost]
    [Authorize]
    [AngularMethod(typeof(void))]
    public async Task<IActionResult> Unfollow(FollowDto data) => Result(await followService.UnfollowAsync(data));

    [HttpPost]
    [Authorize]
    [AngularMethod(typeof(bool))]
    public async Task<IActionResult> IsFollowing(FollowDto data) => Result(await followService.IsFollowingAsync(data));
}