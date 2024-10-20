namespace InSynq.Core.Model;

public interface IDatabaseContextAudit
{
    DbSet<User_aud> User_aud { get; }

    DbSet<LegalDocument_aud> LegalDocument_aud { get; }
}