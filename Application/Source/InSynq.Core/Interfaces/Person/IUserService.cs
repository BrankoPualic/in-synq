using InSynq.Core.Dtos.User;

namespace InSynq.Core.Interfaces.Person;

public interface IUserService
{
    Task<ResponseWrapper<UserDto>> GetSingleAsync(long id);

    Task<ResponseWrapper<PagingResultDto<UserDto>>> SearchAsync(UserSearchOptions options);
}