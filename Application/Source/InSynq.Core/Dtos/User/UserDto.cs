using InSynq.Core.Dtos.ProviderData;
using User_ = InSynq.Core.Model.Models.Application.User.User;

namespace InSynq.Core.Dtos.User;

public class UserDto : BaseDto<UserDto>
{
    public UserDto() : base(new UserDtoValidator())
    {
    }

    public long Id { get; set; }

    public string Username { get; set; }

    public string FullName { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string ProfileImageUrl { get; set; }

    public string Biography { get; set; }

    public DateTime DateOfBirth { get; set; }

    public eGender GenderId { get; set; }

    public LookupValueDto Gender { get; set; }

    public CountryDto Country { get; set; }

    public string Phone { get; set; }

    public bool Privacy { get; set; }

    public long Followers { get; set; }

    public long Following { get; set; }

    public DateTime CreatedOn { get; set; }

    public void ToModel(User_ model)
    {
        model.Username = Username.TrimText();
        model.FirstName = FirstName.TrimText();
        model.MiddleName = MiddleName.TrimText();
        model.LastName = LastName.TrimText();
        model.Biography = Biography.TrimText();
        model.DateOfBirth = DateOfBirth;
        model.GenderId = GenderId;
        model.CountryId = Country.Id;
        model.Phone = Phone.TrimText();
        model.Privacy = Privacy;
    }
}