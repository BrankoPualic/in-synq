using InSynq.Common.Attributes;

namespace InSynq.Core.Dtos._Base;

[TsIgnore]
public abstract class BaseDto<TDto> where TDto : BaseDto<TDto>
{
    protected readonly IValidator<TDto> _validator;

    protected BaseDto(IValidator<TDto> validator = null) => _validator = validator;

    public Error Errors { get; } = new();

    public bool IsValid(eAuditChangeType? type = null)
    {
        try
        {
            if (type == eAuditChangeType.Create)
                ValidateOnCreate();

            if (type == eAuditChangeType.Update)
                ValidateOnUpdate();

            if (type == eAuditChangeType.Delete)
                ValidateOnDelete();

            if (!type.HasValue)
                Validate();
        }
        catch { }

        return !Errors.HasErrors;
    }

    public virtual void ValidateOnCreate() => AddValidationErrors(_validator?.Validate((TDto)this, _ => _.IncludeRuleSets(eAuditChangeType.Create.ToString())));

    public virtual void ValidateOnUpdate() => AddValidationErrors(_validator?.Validate((TDto)this, _ => _.IncludeRuleSets(eAuditChangeType.Update.ToString())));

    public virtual void ValidateOnDelete() => AddValidationErrors(_validator?.Validate((TDto)this, _ => _.IncludeRuleSets(eAuditChangeType.Delete.ToString())));

    public virtual void Validate() => AddValidationErrors(_validator?.Validate((TDto)this));

    protected void AddValidationErrors(ValidationResult result = null) => result?.Errors.ForEach(_ => Errors.AddError(_.PropertyName, _.ErrorMessage));
}