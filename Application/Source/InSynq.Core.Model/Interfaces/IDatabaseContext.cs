namespace InSynq.Core.Model;

public partial interface IDatabaseContext
{
	IDatabaseContext GetDatabaseContext();
}

public partial interface IDatabaseContext : IDatabaseContextBase
{
	DbSet<User> Users { get; set; }

	DbSet<UserRole> UserRoles { get; set; }
}