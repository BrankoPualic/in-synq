using InSynq.Core.Dtos.User;
using InSynq.Core.Model.Models.Application.User;
using Microsoft.AspNetCore.Http;
using User_ = InSynq.Core.Model.Models.Application.User.User;

namespace InSynq.Core.Dtos.Auth;

public class SignupDto : BaseDto
{
    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Biography { get; set; }

    public IFormFile? Photo { get; set; }

    public UserDetailsDto Details { get; set; }

    public void ToModel(User_ model)
    {
        model.FirstName = FirstName;
        model.MiddleName = MiddleName;
        model.LastName = LastName;
        model.Username = Username;
        model.Email = Email;
        model.Biography = Biography;
        model.DateOfBirth = DateOfBirth;
        model.Details = Details.SerializeJsonObject(defaultValueHandling: Newtonsoft.Json.DefaultValueHandling.Include);
        model.IsActive = true;
        model.Roles = [new UserRole { RoleId = eSystemRole.Member }];
    }

    // Validation

    private readonly IValidator<SignupDto> _validator = new SignupDtoValidator();

    public override void ValidateOnCreateOrUpdate() => AddValidationErrors(_validator.Validate(this, _ => _.IncludeRuleSets(eAuditChangeType.Create.ToString())));
}