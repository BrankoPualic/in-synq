namespace InSynq.Infrastructure.Data;

public partial class DatabaseContext : IDatabaseContextAudit
{
    public virtual DbSet<User_aud> User_aud { get; set; }
}