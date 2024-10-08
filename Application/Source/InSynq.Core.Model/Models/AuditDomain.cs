﻿namespace InSynq.Core.Model.Models;

public abstract class AuditDomain<TKey> : IAuditDomainEntity<TKey>
{
    [Key]
    public long AuditId { get; set; }

    public DateTime AuditTimeStamp { get; set; }

    public string LogType { get; set; }

    public TKey Id { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long LastChangedBy { get; set; }

    public DateTime LastChangedOn { get; set; }

    public long? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public eAuditChangeType ChangeType => LogType switch
    {
        Constants.AUDIT_LOG_TYPE_INSERT => eAuditChangeType.Create,
        Constants.AUDIT_LOG_TYPE_UPDATE => eAuditChangeType.Update,
        Constants.AUDIT_LOG_TYPE_DELETE => eAuditChangeType.Delete,
        _ => eAuditChangeType.Update
    };
}