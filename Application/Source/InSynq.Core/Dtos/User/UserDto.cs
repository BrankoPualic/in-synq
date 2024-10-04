using InSynq.Core.Dtos.ProviderData;

namespace InSynq.Core.Dtos.User;

public class UserDto
{
    public long Id { get; set; }

    public string Username { get; set; }

    public string FullName { get; set; }

    public string ProfileImageUrl { get; set; }

    public string Biography { get; set; }

    public LookupValueDto Gender { get; set; }

    public CountryDto Country { get; set; }

    public bool Privacy { get; set; }

    public long Followers { get; set; }

    public long Following { get; set; }
}