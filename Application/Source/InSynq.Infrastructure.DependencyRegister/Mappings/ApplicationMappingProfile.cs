using AutoMapper;
using InSynq.Core.Dtos.Document;
using InSynq.Core.Dtos.ProviderData;
using InSynq.Core.Dtos.User;
using InSynq.Core.Model.Models.Application;
using InSynq.Core.Model.Models.Application.ReferenceData;
using InSynq.Core.Model.Models.Application.User;

namespace InSynq.Infrastructure.DependencyRegister.Mappings;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        // Reference Data
        CreateMap<Country, CountryDto>();
        // Data
        CreateMap<User, UserDto>()
            .ForLookup(_ => _.Gender, _ => _.GenderId);
        CreateMap<LegalDocument, DocumentDto>()
            .ForLookup(_ => _.Type, _ => _.TypeId);
    }
}