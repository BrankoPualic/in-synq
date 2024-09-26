using InSynq.Common.Extensions;

namespace InSynq.Core.Model.Models.Application.User;

public class User : BaseAuditedDomain<long>, IConfigurableEntity
{
	public string FirstName { get; set; }

	public string MiddleName { get; set; }

	public string LastName { get; set; }

	public string FullName => $"{FirstName} {(MiddleName.IsNotNullOrWhiteSpace() ? MiddleName + " " : "")}{LastName}";

	public string Username { get; set; }

	public string Email { get; set; }

	public string Password { get; set; }

	public string ProfileImageUrl { get; set; }

	public string PublicId { get; set; }

	public string Biography { get; set; }

	public DateTime DateOfBirth { get; set; }

	public string Details { get; set; }

	public bool IsActive { get; set; }

	public bool IsLocked { get; set; }

	[InverseProperty(nameof(User))]
	public virtual ICollection<UserRole> Roles { get; set; } = [];

	public void Configure(ModelBuilder builder)
	{
		builder.Entity<User>(_ =>
		{
			_.Property(_ => _.FirstName).HasMaxLength(20).IsRequired();
			_.Property(_ => _.MiddleName).HasMaxLength(20);
			_.Property(_ => _.LastChangedBy).HasMaxLength(30).IsRequired();
			_.Property(_ => _.Username).HasMaxLength(20).IsRequired();
			_.Property(_ => _.Email).HasMaxLength(80).IsRequired();
			_.Property(_ => _.Biography).HasMaxLength(255);

			// indexes
			_.HasIndex(_ => _.Username).IsUnique();
			_.HasIndex(_ => _.Email).IsUnique();
		});
	}
}