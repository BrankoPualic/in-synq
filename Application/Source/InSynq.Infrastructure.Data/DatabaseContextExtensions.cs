namespace InSynq.Infrastructure.Data;

internal static class DatabaseContextExtensions
{
    internal static void SetTableNames(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            entityType.SetTableName(entityType.ClrType.GetTableName());
        }
    }
}