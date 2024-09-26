using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Service;

public class UserManager(IDatabaseContext db) : IUserManager
{
	public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

	public bool VerifyPassword(string password, string storedPassword) => BCrypt.Net.BCrypt.Verify(password, storedPassword);

	public async Task LockUser(string email)
	{
		var model = await db.Users.GetSingleAsync(_ => _.Email == email);
		model.IsLocked = true;
		await db.SaveChangesAsync();
	}

	public async Task UnlockUser(string email)
	{
		var model = await db.Users.GetSingleAsync(_ => _.Email == email);
		if (model.IsLocked == true)
		{
			model.IsLocked = false;
			await db.SaveChangesAsync();
		}
	}

	public async Task<bool> IsUserLocked(string email) => (await db.Users.Where(_ => _.Email == email).Select(_ => _.IsLocked).FirstOrDefaultAsync()) == true;
}