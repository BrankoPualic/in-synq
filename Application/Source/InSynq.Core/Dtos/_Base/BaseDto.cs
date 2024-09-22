using InSynq.Common;
using InSynq.Core.Model;

namespace InSynq.Core.Dtos._Base;

public abstract class BaseDto
{
	public Error Error { get; } = new();

	public bool IsValid(eAuditChangeType? type = null)
	{
		if (type == eAuditChangeType.Create)
			ValidateOnCreate();

		if (type == eAuditChangeType.Update)
			ValidateOnUpdate();

		if (type == eAuditChangeType.Delete)
			ValidateOnDelete();

		if (!type.HasValue)
			ValidateOnCreateOrUpdate();

		return !Error.HasErrors;
	}

	public virtual void ValidateOnCreate()
	{ }

	public virtual void ValidateOnUpdate()
	{ }

	public virtual void ValidateOnDelete()
	{ }

	public virtual void ValidateOnCreateOrUpdate()
	{ }
}