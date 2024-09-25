namespace InSynq.Core.Model.Models;

public class MockIdentityUser : IIdentityUser
{
	public long Id { get; set; } = Constants.SYSTEM_USER;

	public string Username { get; set; } = "Mock User";

	public string Email { get; set; } = "mock.user@insynq.com";

	public bool IsAuthenticated { get; set; } = true;

	public List<eSystemRole> Roles { get; set; } = [eSystemRole.Admin];

	public bool HasRole(List<eSystemRole> roles)
	{
		throw new NotImplementedException();
	}
}