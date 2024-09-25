namespace InSynq.Core;

public interface IUserManager
{
	string HashPassword(string password);

	bool VerifyPassword(string password, string storedPassword);

	Task LockUser(string email);

	Task UnlockUser(string email);

	Task<bool> IsUserLocked(string email);
}