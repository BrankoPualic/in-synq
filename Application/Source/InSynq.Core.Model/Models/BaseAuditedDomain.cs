namespace InSynq.Core.Model.Models;

public abstract class BaseAuditedDomain<TKey> : BaseDomain<TKey>, IAuditedEntity
    where TKey : struct
{
    public DateTime CreatedOn { get; set; }

    public long CreatedBy { get; set; }

    public DateTime LastChangedOn { get; set; }

    public long LastChangedBy { get; set; }
}