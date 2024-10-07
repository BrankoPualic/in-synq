namespace InSynq.Core.Dtos.Auth;

public class SignupDtoValidator : AbstractValidator<SignupDto>
{
    public SignupDtoValidator()
    {
        RuleFor(_ => _.FirstName)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("First Name"))
            .MinimumLength(3).WithMessage(ResourceValidation.MinimumLength.FormatWith("First Name", 3))
            .MaximumLength(20).WithMessage(ResourceValidation.MaximumLength.FormatWith("First Name", 20));

        RuleFor(_ => _.MiddleName)
            .MinimumLength(3).WithMessage(ResourceValidation.MinimumLength.FormatWith("Middle Name", 3))
            .MaximumLength(20).WithMessage(ResourceValidation.MaximumLength.FormatWith("Middle Name", 20))
            .When(_ => _.MiddleName.IsNotNullOrWhiteSpace());

        RuleFor(_ => _.LastName)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Last Name"))
            .MinimumLength(3).WithMessage(ResourceValidation.MinimumLength.FormatWith("Last Name", 3))
            .MaximumLength(30).WithMessage(ResourceValidation.MaximumLength.FormatWith("Last Name", 30));

        RuleFor(_ => _.Email)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Email"))
            .MaximumLength(80).WithMessage(ResourceValidation.MaximumLength.FormatWith("Email", 80))
            .EmailAddress().WithMessage(ResourceValidation.Wrong_Format.FormatWith("Email"));

        RuleFor(_ => _.Username)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Username"))
            .MinimumLength(5).WithMessage(ResourceValidation.MinimumLength.FormatWith("Username", 5))
            .MaximumLength(20).WithMessage(ResourceValidation.MaximumLength.FormatWith("Username", 20));

        RuleFor(_ => _.Password)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Password"))
            .MinimumLength(8).WithMessage(ResourceValidation.MinimumLength.FormatWith("Password", 8))
            .MaximumLength(50).WithMessage(ResourceValidation.MaximumLength.FormatWith("Password", 50))
            .Matches(@"(?=.*[a-z])").WithMessage("Passwrod lacks 1 lowercase letter.")
            .Matches(@"(?=.*[A-Z])").WithMessage("Passwrod lacks 1 uppercase letter.")
            .Matches(@"(?=.*\d)").WithMessage("Passwrod lacks 1 digit.")
            .Matches(@"(?=.*[@$!%*?&])").WithMessage("Passwrod lacks 1 special character.");

        RuleFor(_ => _.ConfirmPassword)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Confirm Password"))
            .Equal(_ => _.Password).WithMessage(ResourceValidation.Dont_Match.FormatWith("Password", "Confirm Password"));

        RuleFor(_ => _.Biography)
            .MaximumLength(255).WithMessage(ResourceValidation.MaximumLength.FormatWith("Biography", 255))
            .When(_ => _.Biography.IsNotNullOrWhiteSpace());

        RuleFor(_ => _.DateOfBirth)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Date of Birth"))
            .Must(Functions.IsValidDate).WithMessage(ResourceValidation.Invalid.FormatWith("Date of Birth"))
            .Must(Functions.AtLeast16YearsOld).WithMessage("Must be at least 16 years old.");

        RuleFor(_ => _.Photo)
            .Must(Functions.IsValidImage).WithMessage(ResourceValidation.File_Wrong_Format.FormatWith("Image", Constants.FILE_IMAGE_EXTENSIONS.Join(", ")))
            .Must(Functions.WithinFileSize).WithMessage(ResourceValidation.File_Too_Large.FormatWith("Image", "10MB"))
            .When(_ => _.Photo.IsNotNullOrEmpty());

        RuleFor(_ => _.GenderId)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Gender"));

        RuleFor(_ => _.Phone)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Phone Number"))
            .MinimumLength(8).WithMessage(ResourceValidation.MinimumLength.FormatWith("Phone Number", 8))
            .MaximumLength(15).WithMessage(ResourceValidation.MaximumLength.FormatWith("Phone Number", 15));

        RuleFor(_ => _.CountryId)
            .NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Country"));
    }
}