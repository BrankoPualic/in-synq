using InSynq.Core.Model.Models.Application.User;

namespace InSynq.Core;

public interface ITokenService
{
    string GenerateJwtToken(User user);
}