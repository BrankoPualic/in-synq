using InSynq.Core.Dtos.ProviderData;
using InSynq.Core.Model;

namespace InSynq.Core.Dtos.User;

public class UserDetailsDto
{
	public eGender GenderId { get; set; }

	public bool Privacy { get; set; }

	public string Phone { get; set; }

	public CountryDto Country { get; set; }
}