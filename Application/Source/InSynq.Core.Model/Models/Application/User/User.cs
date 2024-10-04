using InSynq.Core.Model.Models.Application.ReferenceData;

namespace InSynq.Core.Model.Models.Application.User;

public class User : BaseIndexAuditedDomain<User, long>, IConfigurableEntity
{
    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string FullName { get; private set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string ProfileImageUrl { get; set; }

    public string PublicId { get; set; }

    public string Biography { get; set; }

    public DateTime DateOfBirth { get; set; }

    public eGender GenderId { get; set; }

    public int CountryId { get; set; }

    public string Phone { get; set; }

    public bool Privacy { get; set; }

    public bool IsActive { get; set; }

    public bool IsLocked { get; set; }

    public virtual Country Country { get; set; }

    [InverseProperty(nameof(User))]
    public virtual ICollection<UserRole> Roles { get; set; } = [];

    [InverseProperty(nameof(UserFollow.Follower))]
    public virtual ICollection<UserFollow> UserFollowing { get; set; } = [];

    [InverseProperty(nameof(UserFollow.Following))]
    public virtual ICollection<UserFollow> UserFollowers { get; set; } = [];

    //
    // Indexes
    //

    public static IDatabaseIndex IX_User_Email => new DatabaseIndex(nameof(IX_User_Email))
    {
        Columns = { nameof(Email) },
        IsUnique = true,
    };

    public static IDatabaseIndex IX_User_Username => new DatabaseIndex(nameof(IX_User_Username))
    {
        Columns = { nameof(Username) },
        IsUnique = true,
    };

    public static IDatabaseIndex IX_User_IsActive_IsLocked => new DatabaseIndex(nameof(IX_User_IsActive_IsLocked))
    {
        Columns = { nameof(IsActive), nameof(IsLocked) },
        Include = { nameof(Username), nameof(FirstName), nameof(MiddleName), nameof(LastName) },
    };

    //
    // Configuration
    //

    public void Configure(ModelBuilder builder)
    {
        builder.Entity<User>(_ =>
        {
            _.Property(_ => _.FirstName).HasMaxLength(20).IsRequired();
            _.Property(_ => _.MiddleName).HasMaxLength(20);
            _.Property(_ => _.LastName).HasMaxLength(30).IsRequired();
            _.Property(_ => _.Username).HasMaxLength(20).IsRequired();
            _.Property(_ => _.Email).HasMaxLength(80).IsRequired();
            _.Property(_ => _.Biography).HasMaxLength(255);
            _.Property(_ => _.Phone).HasMaxLength(20).IsRequired();
            _.Property(_ => _.FullName).HasMaxLength(70).HasComputedColumnSql("[FirstName] + CASE WHEN [MiddleName] IS NOT NULL AND [MiddleName] <> '' THEN ' ' + [MiddleName] ELSE '' END + ' ' + [LastName]");
        });
    }
}