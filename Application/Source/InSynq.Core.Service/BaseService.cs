using InSynq.Core.Dtos._Base;

namespace InSynq.Core.Service;

public class BaseService(IDatabaseContext context)
{
    public static readonly Error ERROR_NOT_FOUND = ErrorConstants.ERROR_NOT_FOUND;
    public static readonly Error ERROR_INVALID_OPERATION = ErrorConstants.ERROR_INVALID_OPERATION;
    public static readonly Error ERROR_UNAUTHORIZED = ErrorConstants.ERROR_UNAUTHORIZED;
    public static readonly Error ERROR_INTERNAL_ERROR = ErrorConstants.ERROR_INTERNAL_ERROR;

    public IDatabaseContext db => context;

    public IIdentityUser CurrentUser { get; } = context.CurrentUser;

    public async Task<ResponseWrapper> ValidateAndSaveAsync<TModel, TDto>(TModel model, TDto data, Func<TModel, Task> preStage = null, Func<TModel, Task> postStage = null)
        where TModel : BaseDomain<long>
        where TDto : BaseDto<TDto>
    {
        if (!data.IsValid())
            return new(data.Errors);

        if (preStage != null)
            await preStage(model);

        if (model.IsNew)
            db.Create(model);

        await db.SaveChangesAsync();

        if (postStage != null)
            await postStage(model);

        return new();
    }

    public async Task<ResponseWrapper> ValidateAndDeleteAsync<TModel>(long id, Func<TModel, Task> preStage = null, Func<TModel, Task> postStage = null)
        where TModel : BaseDomain<long>
    {
        var model = await db.Set<TModel>().GetSingleAsync(_ => _.Id == id);
        if (model.IsNullOrEmpty())
            return new(ERROR_NOT_FOUND);

        if (preStage != null)
            await preStage(model);

        db.Remove(model);

        await db.SaveChangesAsync();

        if (postStage != null)
            await postStage(model);

        return new();
    }
}