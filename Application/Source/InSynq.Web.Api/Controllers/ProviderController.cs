using InSynq.Core.Dtos.ProviderData;
using InSynq.Core.Interfaces;
using InSynq.Web.Api.Controllers._Base;
using InSynq.Web.Api.ReinforcedTypings.Generator;
using Microsoft.AspNetCore.Mvc;

namespace InSynq.Web.Api.Controllers;

public class ProviderController(IProviderService providerService) : BaseController
{
    [HttpGet]
    [AngularMethod(typeof(IEnumerable<CountryDto>))]
    public async Task<IActionResult> GetCountries() => Result(await providerService.GetCountriesAsync());
}