using InSynq.Core.Model.Models.Application.User;

namespace InSynq.Core.Model.Models;

public class AuditDomain<TKey> : IAuditDomainEntity<TKey>
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

	[ForeignKey(nameof(CreatedBy))]
	public virtual User CreatedByUser { get; set; }

	[ForeignKey(nameof(LastChangedBy))]
	public virtual User LastChangedByUser { get; set; }

	[ForeignKey(nameof(DeletedBy))]
	public virtual User DeletedByUser { get; set; }

	public eAuditChangeType ChangeType => LogType switch
	{
		Constants.AUDIT_LOG_TYPE_INSERT => eAuditChangeType.Create,
		Constants.AUDIT_LOG_TYPE_UPDATE => eAuditChangeType.Update,
		Constants.AUDIT_LOG_TYPE_DELETE => eAuditChangeType.Delete,
		_ => eAuditChangeType.Update
	};
}