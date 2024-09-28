using InSynq.Core.Dtos.ProviderData;

namespace InSynq.Core.Interfaces;

public interface IProviderService
{
    Task<ResponseWrapper<IEnumerable<CountryDto>>> GetCountriesAsync();
}