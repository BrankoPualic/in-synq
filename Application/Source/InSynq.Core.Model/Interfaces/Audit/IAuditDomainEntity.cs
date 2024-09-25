using InSynq.Core.Model.Models.Application.User;

namespace InSynq.Core.Model.Interfaces.Audit;

public interface IAuditDomainEntity<TKey> : IAuditDomainEntity
{
	TKey Id { get; set; }
}

public interface IAuditDomainEntity : IAuditDomain<long>
{
	User LastChangedByUser { get; set; }

	User CreatedByUser { get; set; }

	User DeletedByUser { get; set; }
}