using InSynq.Core.Dtos.ProviderData;
using Microsoft.EntityFrameworkCore;

namespace InSynq.Core.Service.Services;

public class ProviderService(IDatabaseContext context, IMapper mapper) : BaseService(context), IProviderService
{
    public async Task<ResponseWrapper<IEnumerable<CountryDto>>> GetCountriesAsync()
    {
        var result = await db.Countries.AsNoTracking().ToListAsync();
        return new(mapper.To<CountryDto>(result));
    }
}