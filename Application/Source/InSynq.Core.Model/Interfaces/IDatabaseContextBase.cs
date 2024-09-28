using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InSynq.Core.Model.Interfaces;

public interface IDatabaseContextBase : IDisposable
{
    IIdentityUser CurrentUser { get; }

    // methods

    bool HasChanges();

    void ClearChanges();

    int SaveChanges(bool audit = true);

    Task<int> SaveChangesAsync(bool audit = true, CancellationToken cancellationToken = default);

    // DbContext

    DatabaseFacade Database { get; }

    DbSet<TModel> Set<TModel>() where TModel : class;

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    IModel Model { get; }
}