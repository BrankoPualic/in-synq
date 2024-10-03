using InSynq.Core.Dtos;
using InSynq.Core.Dtos.User;
using InSynq.Core.Interfaces.Person;
using InSynq.Core.Model.Models.Application.User;
using InSynq.Core.Search;
using System.Linq.Expressions;

namespace InSynq.Core.Service.Services.Person;

public class UserService(IDatabaseContext context, IMapper mapper) : BaseService(context), IUserService
{
    public async Task<ResponseWrapper<UserDto>> GetSingleAsync(long id)
    {
        var result = await db.Users.GetSingleAsync(_ => _.Id == id, _ => _.Country);
        return result == null ? new(ERROR_NOT_FOUND) : new(mapper.To<UserDto>(result));
    }

    public async Task<ResponseWrapper<PagingResultDto<UserDto>>> SearchAsync(UserSearchOptions options)
    {
        var filters = new List<Expression<Func<User, bool>>>();

        if (options.Name.IsNotNullOrWhiteSpace())
            filters.Add(_ => _.Username.Contains(options.Name) || _.FullName.Contains(options.Name));

        filters.Add(_ => _.IsActive == options.IsActive);
        filters.Add(_ => _.IsLocked == options.IsLocked);

        var result = await db.Users.SearchAsync(options, _ => _.Username, false, filters);

        return new(new PagingResultDto<UserDto>
        {
            Total = result.Total,
            Data = mapper.To<UserDto>(result.Data)
        });
    }
}