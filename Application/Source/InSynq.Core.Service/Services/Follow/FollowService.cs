using InSynq.Core.Dtos.Follow;
using InSynq.Core.Interfaces.Follow;
using InSynq.Core.Model.Models.Application.User;

namespace InSynq.Core.Service.Services.Follow;

public class FollowService(IDatabaseContext context) : BaseService(context), IFollowService
{
    public async Task<ResponseWrapper> FollowAsync(FollowDto data)
    {
        var following = await db.Users.FindAsync(data.FollowingId);
        var follower = await db.Users.FindAsync(data.FollowerId);
        if (following == null || follower == null)
            return new(ERROR_INVALID_OPERATION);

        var model = new UserFollow();
        data.ToModel(model);

        db.Create(model);
        await db.SaveChangesAsync();

        return new();
    }

    public async Task<ResponseWrapper> UnfollowAsync(FollowDto data)
    {
        var follow = await db.Follows.GetSingleAsync(_ => _.FollowingId == data.FollowingId && _.FollowerId == data.FollowerId);
        if (follow == null)
            return new(ERROR_INVALID_OPERATION);

        db.Remove(follow);
        await db.SaveChangesAsync();

        return new();
    }

    public async Task<ResponseWrapper<bool>> IsFollowingAsync(FollowDto data)
    {
        var follow = await db.Follows.GetSingleAsync(_ => _.FollowingId == data.FollowingId && _.FollowerId == data.FollowerId);
        return new(follow != null);
    }
}