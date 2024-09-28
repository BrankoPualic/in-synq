using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace InSynq.Infrastructure.Data;

public partial class DatabaseContext : DbContext
{
    private readonly string? _connectionString = null;
    private SqlConnection _sqlConnection;
    private ILoggerFactory _loggerFactory;

    public IIdentityUser CurrentUser { get; private set; }

    public IDatabaseContext GetDatabaseContext() => new DatabaseContext(CurrentUser);

    public DatabaseContext(IIdentityUser currentUser) => CurrentUser = currentUser;

    private static readonly object MigrationLocker = new();

    public DatabaseMigrationResult Migrate(bool preview = false)
    {
        lock (MigrationLocker)
        {
            var startTime = DateTime.UtcNow;

            var migrations = Database.GetPendingMigrations().ToList();
            if (!preview && migrations.IsNotNullOrEmpty())
                Database.Migrate();

            var duration = DateTime.UtcNow - startTime;

            var result = $"DATABASE UPDATE - {(migrations.Count > 0 ? $"{migrations.Count} Migrations" : "NO MIGRATIONS")}";

            migrations.Add($"Duration: {duration}s");

            return new DatabaseMigrationResult { Result = result, Migrations = migrations };
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _sqlConnection ??= new SqlConnection
        {
            ConnectionString = _connectionString ?? Settings.Database
        };

        optionsBuilder.UseSqlServer(_sqlConnection, _ => _.CommandTimeout(600).EnableRetryOnFailure())
            .LogTo(_ => Debug.WriteLine(_));

        _loggerFactory ??= LoggerFactory.Create(_ => _.AddDebug());

        optionsBuilder.UseLoggerFactory(_loggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.SetTableNames();
        modelBuilder.ApplyEntityConfigurations(typeof(ModelExtensions).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        if (!EF.IsDesignTime)
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
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

    private class BlankTriggerAddingConvention : IModelFinalizingConvention
    {
        public virtual void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
        {
            foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
            {
                var table = StoreObjectIdentifier.Create(entityType, StoreObjectType.Table);
                if (table != null && entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(table.Value) == null))
                {
                    entityType.Builder.HasTrigger(table.Value.Name + "_Trigger");
                }

                foreach (var fragment in entityType.GetMappingFragments(StoreObjectType.Table))
                {
                    if (entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(fragment.StoreObject) == null))
                    {
                        entityType.Builder.HasTrigger(fragment.StoreObject.Name + "_Trigger");
                    }
                }
            }
        }
    }

    public override void Dispose()
    {
        _sqlConnection?.Dispose();
        _loggerFactory?.Dispose();

        base.Dispose();
    }

    public override ValueTask DisposeAsync()
    {
        _sqlConnection?.Dispose();
        _loggerFactory?.Dispose();

        return base.DisposeAsync();
    }
}