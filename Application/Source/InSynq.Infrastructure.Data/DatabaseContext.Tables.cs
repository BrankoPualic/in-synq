using InSynq.Core.Model.Models.Application;
using InSynq.Core.Model.Models.Application.ReferenceData;
using InSynq.Core.Model.Models.Application.User;

namespace InSynq.Infrastructure.Data;

public partial class DatabaseContext : IDatabaseContext
{
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserSigninLog> Logins { get; set; }

    public virtual DbSet<UserFollow> Follows { get; set; }

    public virtual DbSet<LegalDocument> LegalDocuments { get; set; }

    // Reference Data

    public virtual DbSet<Country> Countries { get; set; }
}