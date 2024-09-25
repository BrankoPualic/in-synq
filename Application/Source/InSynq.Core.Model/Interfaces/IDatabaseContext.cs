using InSynq.Core.Model.Models.Application.ReferenceData;
using InSynq.Core.Model.Models.Application.User;

namespace InSynq.Core.Model;

public partial interface IDatabaseContext
{
	IDatabaseContext GetDatabaseContext();
}

public partial interface IDatabaseContext : IDatabaseContextBase, IDatabaseContextAudit
{
	DbSet<User> Users { get; }

	DbSet<UserRole> UserRoles { get; }

	DbSet<UserSigninLog> Logins { get; }

	// Reference Data

	DbSet<Country> Countries { get; }
}