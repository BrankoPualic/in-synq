using InSynq.Core.Dtos.Follow;

namespace InSynq.Core.Interfaces.Follow;

public interface IFollowService
{
    Task<ResponseWrapper> FollowAsync(FollowDto data);

    Task<ResponseWrapper> UnfollowAsync(FollowDto data);

    Task<ResponseWrapper<bool>> IsFollowingAsync(FollowDto data);
}