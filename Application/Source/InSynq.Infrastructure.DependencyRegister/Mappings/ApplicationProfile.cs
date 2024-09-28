using AutoMapper;
using InSynq.Core.Dtos.ProviderData;
using InSynq.Core.Model.Models.Application.ReferenceData;

namespace InSynq.Infrastructure.DependencyRegister.Mappings;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        // Reference Data
        CreateMap<Country, CountryDto>();
    }
}