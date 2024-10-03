using InSynq.Core.Model.Models.Application.User;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InSynq.Core.Service.Functions;

public static class FullTextFilter
{
    // Generic filter
    public static Expression<Func<TModel, bool>> Get<TModel>(string filter) where TModel : ISearchable => !filter.ToFullTextSearch(out var search) ? null : _ => EF.Functions.Contains(_.SearchPattern, search);

    // Model specifuc filters
    public static Expression<Func<User, bool>> User(string filter) => !filter.ToFullTextSearch(out var search) ? null : _ => EF.Functions.Contains(_.SearchPattern, search);
}