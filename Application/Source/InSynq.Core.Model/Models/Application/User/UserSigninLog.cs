namespace InSynq.Core.Model.Models.Application.User;

public class UserSigninLog : BaseDomain<long>
{
	public UserSigninLog(long userId)
	{
		UserId = userId;
		CreatedOn = DateTime.UtcNow;
	}

	public long UserId { get; set; }

	public DateTime CreatedOn { get; set; }

	[ForeignKey(nameof(UserId))]
	public virtual User User { get; set; }
}