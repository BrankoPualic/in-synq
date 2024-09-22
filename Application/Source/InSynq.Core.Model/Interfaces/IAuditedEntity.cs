namespace InSynq.Core.Model.Interfaces;

public interface IAuditedEntity
{
	DateTime CreatedOn { get; set; }

	long CreatedBy { get; set; }

	DateTime LastChangedOn { get; set; }

	long LastChangedBy { get; set; }

	User CreatedByUser { get; set; }

	User LastChangedByUser { get; set; }
}