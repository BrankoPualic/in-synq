namespace InSynq.Infrastructure.Data;

public partial class DatabaseContext : IDatabaseContextAudit
{
    public virtual DbSet<User_aud> User_aud { get; set; }

    public virtual DbSet<LegalDocument_aud> LegalDocument_aud { get; set; }
}