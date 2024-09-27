namespace InSynq.Core.Model.Interfaces.Audit;

public interface IAuditDomainEntity<TKey> : IAuditDomainEntity
{
	TKey Id { get; set; }
}

public interface IAuditDomainEntity : IAuditDomain<long>
{
}