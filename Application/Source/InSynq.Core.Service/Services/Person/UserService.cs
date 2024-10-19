using InSynq.Core.Dtos;
using InSynq.Core.Dtos.User;
using InSynq.Core.Interfaces.Person;
using InSynq.Core.Model.Models.Application.User;
using InSynq.Core.Search;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InSynq.Core.Service.Services.Person;

public class UserService(IDatabaseContext context, IMapper mapper) : BaseService(context), IUserService
{
    public async Task<ResponseWrapper<UserDto>> GetCurrentUserAsync()
    {
        var result = await db.Users.GetSingleAsync(_ => _.Id == CurrentUser.Id, _ => _.Country);
        return result == null ? new(ERROR_NOT_FOUND) : new(mapper.To<UserDto>(result));
    }

    public async Task<ResponseWrapper<UserDto>> GetSingleAsync(long id)
    {
        var result = await db.Users.GetSingleAsync(_ => _.Id == id, _ => _.Country);
        if (result == null)
            return new(ERROR_NOT_FOUND);

        var follows = await db.Follows
            .Where(_ => _.FollowerId == id || _.FollowingId == id)
            .GroupBy(_ => _.FollowingId == id)
            .Select(_ => new
            {
                Following = _.Count(_ => _.FollowerId == id),
                Followers = _.Count(_ => _.FollowingId == id)
            })
            .FirstOrDefaultAsync();

        var data = mapper.To<UserDto>(result);
        data.Followers = follows?.Followers ?? 0;
        data.Following = follows?.Following ?? 0;

        return new(data);
    }

    public async Task<ResponseWrapper<UserLogDto>> GetUserLogAsync(long id)
    {
        var result = await db.User_aud
            .Where(_ => _.Id == id)
            .Select(_ => _.Username)
            .Distinct()
            .ToListAsync();

        return new(new UserLogDto { Usernames = result, UsernameCount = result.Count });
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

    public async Task<ResponseWrapper> UpdateAsync(UserDto data)
    {
        if (!data.IsValid())
            return new(data.Errors);

        var model = await db.Users.FindAsync(data.Id);
        data.ToModel(model);
        await db.SaveChangesAsync();
        return new();
    }
}