namespace InSynq.Core.Model.Models.Application.User;

[PrimaryKey(nameof(UserId), nameof(RoleId))]
public class UserRole
{
	public long UserId { get; set; }

	public eSystemRole RoleId { get; set; }

	[ForeignKey(nameof(UserId))]
	public User User { get; set; }
}