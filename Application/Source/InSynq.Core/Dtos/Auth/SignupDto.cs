using InSynq.Core.Model.Models.Application.User;
using Microsoft.AspNetCore.Http;
using User_ = InSynq.Core.Model.Models.Application.User.User;

namespace InSynq.Core.Dtos.Auth;

public class SignupDto : BaseDto<SignupDto>
{
    public SignupDto() : base(new SignupDtoValidator())
    {
    }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Biography { get; set; }

    public IFormFile Photo { get; set; }

    public eGender GenderId { get; set; }

    public int CountryId { get; set; }

    public string Phone { get; set; }

    public bool Privacy { get; set; }

    public void ToModel(User_ model)
    {
        model.FirstName = FirstName.TrimText();
        model.MiddleName = MiddleName.TrimText();
        model.LastName = LastName.TrimText();
        model.Username = Username.TrimText();
        model.Email = Email.TrimText();
        model.Biography = Biography.TrimText();
        model.DateOfBirth = DateOfBirth;
        model.GenderId = GenderId;
        model.CountryId = CountryId;
        model.Phone = Phone.TrimText();
        model.Privacy = Privacy;
        model.IsActive = true;
        model.Roles = [new UserRole { RoleId = eSystemRole.Member }];
    }
}