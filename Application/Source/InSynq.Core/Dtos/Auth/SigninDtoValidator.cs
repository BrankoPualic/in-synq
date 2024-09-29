namespace InSynq.Core.Dtos.Auth;

public class SigninDtoValidator : AbstractValidator<SigninDto>
{
    public SigninDtoValidator()
    {
        RuleFor(_ => _.Email)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Email"));
        RuleFor(_ => _.Email)
            .MaximumLength(80).WithMessage(ResourceValidation.MaximumLength.FormatWith("Email", 80))
            .EmailAddress().WithMessage(ResourceValidation.Wrong_Format.FormatWith("Email"))
            .When(_ => _.Email.IsNotNullOrWhiteSpace());

        RuleFor(_ => _.Password)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Password"));
        RuleFor(_ => _.Password)
            .MinimumLength(8).WithMessage(ResourceValidation.MinimumLength.FormatWith("Password", 8))
            .MaximumLength(50).WithMessage(ResourceValidation.MaximumLength.FormatWith("Password", 50))
            .When(_ => _.Password.IsNotNullOrWhiteSpace());
    }
}