namespace InSynq.Core.Model.Interfaces.Audit;

public interface IAuditDomain<TKey> where TKey : struct
{
    DateTime AuditTimeStamp { get; set; }

    string LogType { get; set; }

    DateTime CreatedOn { get; set; }

    TKey CreatedBy { get; set; }

    DateTime LastChangedOn { get; set; }

    TKey LastChangedBy { get; set; }

    DateTime? DeletedOn { get; set; }

    TKey? DeletedBy { get; set; }

    eAuditChangeType ChangeType { get; }
}