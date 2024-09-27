using Microsoft.AspNetCore.Http;

namespace InSynq.Core.Dtos.Auth;

public class SignupDtoValidator : AbstractValidator<SignupDto>
{
	public SignupDtoValidator()
	{
		RuleSet(eAuditChangeType.Create.ToString(), () =>
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
				.Matches(@"(?=.*[a-z])").WithMessage("At least 1 lowercase letter.")
				.Matches(@"(?=.*[A-Z])").WithMessage("At least 1 uppercase letter.")
				.Matches(@"(?=.*\d)").WithMessage("At least 1 digit.")
				.Matches(@"(?=.*[@$!%*?&])").WithMessage("At least 1 special character.");

			RuleFor(_ => _.ConfirmPassword)
				.NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Confirm Password"))
				.Equal(_ => _.Password).WithMessage(ResourceValidation.Dont_Match.FormatWith("Password", "Confirm Password"));

			RuleFor(_ => _.Biography)
				.MaximumLength(255).WithMessage(ResourceValidation.MaximumLength.FormatWith("Biography", 255))
				.When(_ => _.Biography.IsNotNullOrWhiteSpace());

			RuleFor(_ => _.DateOfBirth)
				.Must(Functions.IsValidDate).WithMessage(ResourceValidation.Invalid.FormatWith("Date of Birth"))
				.Must(AtLeast16YearsOld).WithMessage("Must be at least 16 years old.");

			RuleFor(_ => _.Photo)
				.Must(Functions.IsValidImage).WithMessage(ResourceValidation.File_Wrong_Format.FormatWith("Image", Constants.FILE_IMAGE_EXTENSIONS.Join(", ")))
				.Must(WithinFileSize).WithMessage(ResourceValidation.File_Too_Large.FormatWith("Image", "10MB"))
				.When(_ => _.Photo.IsNotNullOrEmpty());

			// User Details
			RuleFor(_ => _.Details.Phone)
				.NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Phone Number"))
				.MinimumLength(8).WithMessage(ResourceValidation.MinimumLength.FormatWith("Phone Number", 8))
				.MaximumLength(15).WithMessage(ResourceValidation.MaximumLength.FormatWith("Phone Number", 15));

			RuleFor(_ => _.Details.CountryId)
				.NotEmpty().WithMessage(ResourceValidation.Required.FormatWith("Country"))
				.Must(_ => _ != default).WithMessage(ResourceValidation.Required.FormatWith("Country"));
		});
	}

	private bool AtLeast16YearsOld(DateTime dob) => dob <= DateTime.Today.AddYears(-16);

	private bool WithinFileSize(IFormFile image) => image.Length <= Constants.FILE_SIZE_10MB;
}