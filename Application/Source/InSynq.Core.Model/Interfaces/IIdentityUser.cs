namespace InSynq.Core.Model.Interfaces;

public interface IIdentityUser
{
	long Id { get; }

	string Email { get; }

	string Username { get; }

	List<eSystemRole> Roles { get; }

	bool IsAuthenticated { get; }

	bool HasRole(List<eSystemRole> roles);
}