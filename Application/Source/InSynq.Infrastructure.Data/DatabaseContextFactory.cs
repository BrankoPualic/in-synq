using Microsoft.EntityFrameworkCore.Design;

namespace InSynq.Infrastructure.Data;

internal class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        IIdentityUser identityUser = null;
        return new DatabaseContext(identityUser);
    }
}