using InSynq.Core.Model.Models.Application.User;

namespace InSynq.Core.Dtos.Follow;

public class FollowDto
{
    public long FollowerId { get; set; }

    public long FollowingId { get; set; }

    public void ToModel(UserFollow model)
    {
        model.FollowerId = FollowerId;
        model.FollowingId = FollowingId;
        model.FollowDate = DateTime.UtcNow;
    }
}