using InSynq.Core.Model;
using InSynq.Web.Api.ReinforcedTypings.Generator;

namespace InSynq.Web.Api.ReinforcedTypings;

public enum Providers
{
    [EnumProvider<eGender>]
    Genders,

    [EnumProvider<eSystemRole>]
    SystemRoles,
}