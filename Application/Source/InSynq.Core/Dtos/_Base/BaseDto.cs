using InSynq.Common.Attributes;

namespace InSynq.Core.Dtos._Base;

[TsIgnore]
public abstract class BaseDto
{
	public Error Errors { get; } = new();

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

		return !Errors.HasErrors;
	}

	public virtual void ValidateOnCreate()
	{ }

	public virtual void ValidateOnUpdate()
	{ }

	public virtual void ValidateOnDelete()
	{ }

	public virtual void ValidateOnCreateOrUpdate()
	{ }

	protected void AddValidationErrors(ValidationResult result) => result.Errors.ForEach(_ => Errors.AddError(_.PropertyName, _.ErrorMessage));
}