using InSynq.Core.Model.Interfaces;

namespace InSynq.Infrastructure.Data;

public partial class DatabaseContext : DbContext
{
	private readonly string? _connectionString = null;

	public IIdentityUser CurrentUser { get; private set; }

	public IDatabaseContext GetDatabaseContext() => new DatabaseContext(CurrentUser);

	public DatabaseContext(IIdentityUser currentUser) => CurrentUser = currentUser;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var connection = new SqlConnection
		{
			ConnectionString = _connectionString ?? Settings.Database
		};

		optionsBuilder.UseSqlServer(connection, _ => _.CommandTimeout(600).EnableRetryOnFailure().UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("dbo");
		//modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		modelBuilder.SetTableNames();
		modelBuilder.ApplyEntityConfigurations(typeof(ModelExtensions).Assembly);

		base.OnModelCreating(modelBuilder);
	}

	// methods

	public bool HasChanges() => ChangeTracker.HasChanges();

	public void ClearChanges() => ChangeTracker.Clear();

	public new int SaveChanges(bool audit = true)
	{
		RunAudit(audit);

		return base.SaveChanges();
	}

	public new async Task<int> SaveChangesAsync(bool audit = true, CancellationToken cancellationToken = default)
	{
		RunAudit(audit);

		var result = await base.SaveChangesAsync(cancellationToken);

		return result;
	}

	// private

	private void RunAudit(bool audit)
	{
		if (!audit || !HasChanges())
			return;

		var changedEntries = ChangeTracker.Entries()
			.Where(_ => _.State.In(EntityState.Modified, EntityState.Added)
			&& _.Entity.GetType().GetInterface(nameof(IAuditedEntity)) != null)
			.ToList();

		var (now, userId) = GetAuditInfo();

		foreach (var changedEntry in changedEntries)
		{
			var entity = (IAuditedEntity)changedEntry.Entity;

			entity.LastChangedBy = userId;
			entity.LastChangedOn = now;

			if (changedEntry.State == EntityState.Added)
			{
				entity.CreatedBy = userId;
				entity.CreatedOn = now;
			}
		}
	}

	private (DateTime now, long userId) GetAuditInfo()
	{
		var now = DateTime.UtcNow;

		var userId = CurrentUser.Id;
		if (userId == default)
			userId = Constants.SYSTEM_USER;

		return (now, userId);
	}
}