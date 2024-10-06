using InSynq.Core.Dtos.ProviderData;
using User_ = InSynq.Core.Model.Models.Application.User.User;

namespace InSynq.Core.Dtos.User;

public class UserDto
{
    public long Id { get; set; }

    public string Username { get; set; }

    public string FullName { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string ProfileImageUrl { get; set; }

    public string Biography { get; set; }

    public eGender GenderId { get; set; }

    public LookupValueDto Gender { get; set; }

    public CountryDto Country { get; set; }

    public bool Privacy { get; set; }

    public long Followers { get; set; }

    public long Following { get; set; }

    public DateTime CreatedOn { get; set; }

    public void ToModel(User_ model)
    {
        // BPR: Validate fields on update
        model.Username = Username;
        model.FirstName = FirstName;
        model.MiddleName = MiddleName;
        model.LastName = LastName;
        model.Biography = Biography;
        model.GenderId = GenderId;
        model.CountryId = Country.Id;
        model.Privacy = Privacy;
    }
}