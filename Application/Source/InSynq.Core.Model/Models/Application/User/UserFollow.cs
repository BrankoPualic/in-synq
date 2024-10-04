namespace InSynq.Core.Model.Models.Application.User;

[PrimaryKey(nameof(FollowerId), nameof(FollowingId))]
public class UserFollow
{
    public long FollowerId { get; set; }

    public long FollowingId { get; set; }

    public DateTime FollowDate { get; set; }

    [ForeignKey(nameof(FollowerId))]
    public virtual User Follower { get; set; }

    [ForeignKey(nameof(FollowingId))]
    public virtual User Following { get; set; }
}