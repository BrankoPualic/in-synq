namespace InSynq.Core.Dtos.User;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(_ => _.FirstName)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("First Name"));
        RuleFor(_ => _.FirstName)
            .MinimumLength(3).WithMessage(ResourceValidation.MinimumLength.FormatWith("First Name", 3))
            .MaximumLength(20).WithMessage(ResourceValidation.MaximumLength.FormatWith("First Name", 20))
            .When(_ => _.FirstName.IsNotNullOrWhiteSpace());

        RuleFor(_ => _.MiddleName)
            .MinimumLength(3).WithMessage(ResourceValidation.MinimumLength.FormatWith("Middle Name", 3))
            .MaximumLength(20).WithMessage(ResourceValidation.MaximumLength.FormatWith("Middle Name", 20))
            .When(_ => _.MiddleName.IsNotNullOrWhiteSpace());

        RuleFor(_ => _.LastName)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Last Name"));
        RuleFor(_ => _.LastName)
            .MinimumLength(3).WithMessage(ResourceValidation.MinimumLength.FormatWith("Last Name", 3))
            .MaximumLength(30).WithMessage(ResourceValidation.MaximumLength.FormatWith("Last Name", 30))
            .When(_ => _.LastName.IsNotNullOrWhiteSpace());

        RuleFor(_ => _.Username)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Username"));
        RuleFor(_ => _.Username)
            .MinimumLength(5).WithMessage(ResourceValidation.MinimumLength.FormatWith("Username", 5))
            .MaximumLength(20).WithMessage(ResourceValidation.MaximumLength.FormatWith("Username", 20))
            .When(_ => _.Username.IsNotNullOrWhiteSpace());

        RuleFor(_ => _.Biography)
           .MaximumLength(255).WithMessage(ResourceValidation.MaximumLength.FormatWith("Biography", 255))
           .When(_ => _.Biography.IsNotNullOrWhiteSpace());

        RuleFor(_ => _.DateOfBirth)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Date of Birth"))
            .Must(Functions.IsValidDate).WithMessage(ResourceValidation.Invalid.FormatWith("Date of Birth"))
            .Must(Functions.AtLeast16YearsOld).WithMessage("Must be at least 16 years old.");

        RuleFor(_ => _.GenderId)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Gender"));

        RuleFor(_ => _.Phone)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Phone Number"));
        RuleFor(_ => _.Phone)
            .MinimumLength(8).WithMessage(ResourceValidation.MinimumLength.FormatWith("Phone Number", 8))
            .MaximumLength(15).WithMessage(ResourceValidation.MaximumLength.FormatWith("Phone Number", 15))
            .When(_ => _.Phone.IsNotNullOrWhiteSpace());

        RuleFor(_ => _.Country)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Country"));
    }
}