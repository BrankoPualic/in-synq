using InSynq.Core.Dtos.ProviderData;
using InSynq.Core.Interfaces;

namespace InSynq.Core.Service.Services;

public class ProviderService(IDatabaseContext context, IMapper mapper) : BaseService(context), IProviderService
{
	public async Task<ResponseWrapper<IEnumerable<CountryDto>>> GetCountriesAsync()
	{
		var result = await db.Countries.GetListAsync();
		return new(mapper.To<CountryDto>(result));
	}
}