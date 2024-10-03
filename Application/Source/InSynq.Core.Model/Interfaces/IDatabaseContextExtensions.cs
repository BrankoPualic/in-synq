using InSynq.Common.Extensions;
using InSynq.Common.Search;
using InSynq.Common.Sorting;
using InSynq.Core.Model.Models;
using System.Linq.Expressions;

namespace InSynq.Core.Model;

public static class IDatabaseContextExtensions
{
    public static async Task<TModel> GetSingleAsync<TModel>(this DbSet<TModel> dbSet, params object[] keyValue) where TModel : class =>
        await dbSet.FindAsync(keyValue);

    public static async Task<TModel> GetSingleAsync<TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class
    {
        if (includeProperties == null)
            return await dbSet.FirstOrDefaultAsync(predicate);

        var query = includeProperties.Aggregate(dbSet.AsQueryable(), (current, property) => current.IncludeConvert(property));

        return await query.FirstOrDefaultAsync(predicate);
    }

    public static async Task<TModel> GetSingleAsSplitQueryAsync<TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class
    {
        if (includeProperties == null)
            return await dbSet.FirstOrDefaultAsync(predicate);

        var query = includeProperties.Aggregate(dbSet.AsQueryable(), (current, property) => current.IncludeConvert(property));

        return await query.AsSplitQuery().FirstOrDefaultAsync(predicate);
    }

    public static async Task<TModel> GetSingleOrDefaultAsync<TModel>(this DbSet<TModel> dbSet, IBaseDomain<long> data, params Expression<Func<TModel, object>>[] includeProperties) where TModel : BaseDomain<long>, new() =>
        data.Id == 0
            ? new()
            : await dbSet.GetSingleAsync(_ => _.Id == data.Id, includeProperties);

    public static async Task<List<TModel>> GetListAsync<TModel>(this DbSet<TModel> dbSet, CancellationToken cancellationToken, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class =>
        await dbSet.GetQueryable(_ => true, 0, includeProperties).ToListAsync(cancellationToken);

    public static async Task<List<TModel>> GetListAsync<TModel>(this DbSet<TModel> dbSet, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class =>
        await dbSet.GetQueryable(_ => true, 0, includeProperties).ToListAsync();

    public static async Task<List<TModel>> GetListAsync<TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class =>
        await dbSet.GetQueryable(predicate, 0, includeProperties).ToListAsync();

    public static async Task<List<TModel>> GetListAsSplitQueryAsync<TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class =>
        await dbSet.GetQueryable(predicate, 0, includeProperties).AsSplitQuery().ToListAsync();

    public static async Task<List<TModel>> GetListAsync<TModel>(this DbSet<TModel> dbSet, CancellationToken cancellationToken, List<Expression<Func<TModel, bool>>> predicates, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class =>
        await dbSet.GetQueryable(predicates, includeProperties).ToListAsync(cancellationToken);

    public static async Task<List<TModel>> GetListAsync<TModel>(this DbSet<TModel> dbSet, List<Expression<Func<TModel, bool>>> predicates, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class =>
        await dbSet.GetQueryable(predicates, includeProperties).ToListAsync();

    public static async Task<List<TModel>> GetListAsync<TProperty, TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, Expression<Func<TModel, TProperty>> orderBy, bool desc = false, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class =>
        await dbSet.GetQueryable(predicate, orderBy, 0, 0, desc, includeProperties).ToListAsync();

    public static async Task<List<TModel>> GetListAsync<TProperty, TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, Expression<Func<TModel, TProperty>> orderBy, int skip, int take, bool desc = false, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class =>
        await dbSet.GetQueryable(predicate, orderBy, skip, take, desc, includeProperties).ToListAsync();

    public static async Task<List<TResult>> GetSelectAsync<TResult, TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, Expression<Func<TModel, TResult>> select, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class =>
        await dbSet.GetQueryable(predicate, select, includeProperties).ToListAsync();

    public static async Task<long> CountAsync<TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class =>
        await dbSet.GetQueryable(predicate, 0, includeProperties).LongCountAsync();

    public static async Task<PagingResult<TModel>> SearchAsync<TProperty, TModel>(this DbSet<TModel> dbSet, SearchOptions options, Expression<Func<TModel, TProperty>> defaultOrder, bool desc, List<Expression<Func<TModel, bool>>> predicates, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class =>
        await dbSet.SearchAsync(default, options, defaultOrder, desc, _ => _, predicates, includeProperties);

    public static async Task<PagingResult<TModel>> SearchAsync<TProperty, TModel>(this DbSet<TModel> dbSet, CancellationToken cancellationToken, SearchOptions options, Expression<Func<TModel, TProperty>> defaultOrder, bool desc, List<Expression<Func<TModel, bool>>> predicates, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class =>
        await dbSet.SearchAsync(cancellationToken, options, defaultOrder, desc, _ => _, predicates, includeProperties);

    public static async Task<PagingResult<TResult>> SearchAsync<TResult, TProperty, TModel>(this DbSet<TModel> dbSet, SearchOptions options, Expression<Func<TModel, TProperty>> defaultOrder, bool desc, Expression<Func<TModel, TResult>> select, List<Expression<Func<TModel, bool>>> predicates, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class =>
        await dbSet.SearchAsync(default, options, defaultOrder, desc, select, predicates, includeProperties);

    public static async Task<PagingResult<TResult>> SearchAsync<TResult, TProperty, TModel>(this DbSet<TModel> dbSet, CancellationToken cancellationToken, SearchOptions options, Expression<Func<TModel, TProperty>> defaultOrder, bool desc, Expression<Func<TModel, TResult>> select, List<Expression<Func<TModel, bool>>> predicates, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class
    {
        var query = dbSet.AsQueryable();
        query = predicates.Aggregate(query, (current, expression) => current.Where(expression));

        var total = options.TotalCount == false ? 0 : await query.CountAsync(cancellationToken);

        if (includeProperties != null)
            query = includeProperties.Aggregate(query, (current, property) => current.IncludeConvert(property));

        query = options.SortingOptions.IsNotNullOrEmpty()
            ? query.OrderBy(options.SortingOptions)
            : desc
            ? query.OrderByDescending(defaultOrder)
            : query.OrderBy(defaultOrder);

        if (options.Take != 0)
            query = query.Skip(options.Skip).Take(options.Take);

        return new PagingResult<TResult>
        {
            Total = total,
            Data = await query.Select(select).ToListAsync(cancellationToken)
        };
    }

    // Context Extensions
    public static void Create<TModel>(this IDatabaseContextBase db, TModel model) where TModel : class => db.Set<TModel>().Add(model);

    public static void DeleteSingle<TModel>(this IDatabaseContextBase db, TModel model) where TModel : class
    {
        db.AuditDelete(model);
        db.Set<TModel>().Remove(model);
    }

    public static void DeleteSingle<TModel>(this IDatabaseContextBase db, Expression<Func<TModel, bool>> filter)
        where TModel : class
    {
        var entity = db.Set<TModel>().SingleOrDefault(filter)
            ?? throw new ArgumentNullException(typeof(TModel).Name);

        db.DeleteSingle(entity);
    }

    public static void Remove<TModel>(this IDatabaseContextBase db, TModel model) where TModel : class =>
        db.Set<TModel>().Remove(model);

    // private

    private static void AuditDelete<TModel>(this IDatabaseContextBase db, TModel model) where TModel : class
    {
        var auditType = GetAuditType(db, model);

        var userId = db.CurrentUser.Id;
        var now = DateTime.UtcNow;

        var auditEntity = (IAuditDomain<long>)Activator.CreateInstance(auditType);

        auditEntity.AssignProperties(model);
        auditEntity.DeletedBy = userId;
        auditEntity.DeletedOn = now;
        auditEntity.LogType = Constants.AUDIT_LOG_TYPE_DELETE;
        auditEntity.AuditTimeStamp = now;

        db.Entry(auditEntity).State = EntityState.Added;
    }

    private static Type GetAuditType<TModel>(IDatabaseContextBase db, TModel model)
    {
        var type = db.Model.FindRuntimeEntityType(model.GetType());
        var entityType = type.ClrType;
        var auditType = entityType.Assembly.GetType($"InSynq.Core.Model.Models.Audit.{entityType.Name}_aud");

        if (auditType == null)
            throw new InvalidOperationException($"Missing audit model for {entityType.FullName}");

        return auditType;
    }

    private static IQueryable<TModel> IncludeConvert<TModel>(this IQueryable<TModel> query, Expression<Func<TModel, object>> includeProperty) where TModel : class
    => TryParsePath(includeProperty.Body, out var path)
        ? query.Include(path)
        : throw new ArgumentException($"Invalid include parameter: {includeProperty}");

    private static bool TryParsePath(Expression expression, out string path)
    {
        path = null;
        var withoutConvert = expression.RemoveConvert();

        if (withoutConvert is MemberExpression memeberExpression)
        {
            var thisPart = memeberExpression.Member.Name;
            //if (!TryParsePath(memeberExpression.Expression, out var parentPart))
            //	return false;
            TryParsePath(memeberExpression.Expression, out var parentPart);
            path = parentPart == null ? thisPart : $"{parentPart}.{thisPart}";
            return true;
        }
        else if (withoutConvert is MethodCallExpression callExpression)
        {
            if (callExpression.Method.Name == "Select" && callExpression.Arguments.Count == 2)
            {
                if (!TryParsePath(callExpression.Arguments[0], out var parentPart))
                    return false;

                if (parentPart != null)
                    if (callExpression.Arguments[1] is LambdaExpression subExpression)
                    {
                        if (!TryParsePath(subExpression.Body, out var thisPart))
                            return false;
                        if (thisPart != null)
                        {
                            path = $"{parentPart}.{thisPart}";
                            return true;
                        }
                    }
            }
            return false;
        }
        return false;
    }

    private static Expression RemoveConvert(this Expression expression)
    {
        while (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked)
            expression = ((UnaryExpression)expression).Operand;
        return expression;
    }

    private static IQueryable<TModel> GetQueryable<TModel>(this DbSet<TModel> dbSet, List<Expression<Func<TModel, bool>>> predicates, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class
    {
        var query = dbSet.AsQueryable();
        query = predicates.Aggregate(query, (current, expression) => current.Where(expression));

        if (includeProperties != null)
            query = includeProperties.Aggregate(query, (current, property) => current.IncludeConvert(property));

        return query;
    }

    private static IQueryable<TResult> GetQueryable<TResult, TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, Expression<Func<TModel, TResult>> select, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class
    {
        var query = dbSet.Where(predicate);

        if (includeProperties != null)
            query = includeProperties.Aggregate(query, (current, property) => current.IncludeConvert(property));

        return query.Select(select);
    }

    private static IQueryable<TModel> GetQueryable<TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, int take, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class
    {
        var query = dbSet.Where(predicate);

        if (includeProperties != null)
            query = includeProperties.Aggregate(query, (current, property) => current.IncludeConvert(property));

        if (take != 0)
            query = query.Take(take);

        return query;
    }

    private static IQueryable<TModel> GetQueryable<TProperty, TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, Expression<Func<TModel, TProperty>> orderBy, int skip, int take, bool desc = false, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class
    {
        var query = dbSet.Where(predicate);

        if (includeProperties != null)
            query = includeProperties.Aggregate(query, (current, property) => current.IncludeConvert(property));

        query = desc
            ? query.OrderByDescending(orderBy).AsQueryable()
            : query.OrderBy(orderBy).AsQueryable();

        if (skip != 0)
            query = query.Skip(skip);

        if (take != 0)
            query = query.Take(take);

        return query;
    }

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, IEnumerable<SortingOptions> options)
    {
        // assert not null or emptu otpions
        if (options.IsNullOrEmpty())
            throw new ArgumentException($"Invalid sorting of {typeof(T).Name}.");

        var firstOption = options.First();
        var order = GetDynamicOrder<T>(firstOption);

        var orderedQuery = AddOrder(query, orderBy: order, desc: firstOption.Desc);

        foreach (var option in options.Skip(1))
        {
            var thenBy = GetDynamicOrder<T>(option);
            orderedQuery = AddThenOrder(orderedQuery, orderBy: thenBy, desc: option.Desc);
        }

        return (IQueryable<T>)orderedQuery;
    }

    private static dynamic GetDynamicOrder<T>(SortingOptions sorting)
    {
        var entity = Expression.Parameter(typeof(T));
        var property = GetOrderByProperty<T>(entity, sorting.Field);
        var funcType = typeof(Func<,>).MakeGenericType(typeof(T), property.Type);

        dynamic lambda = Expression.Lambda(delegateType: funcType, body: property, parameters: entity);

        return lambda;
    }

    private static MemberExpression GetOrderByProperty<T>(Expression entity, string field)
    {
        var parts = field.Split('.');
        var propertyInfo = typeof(T).GetProperty(parts.First());
        var property = Expression.PropertyOrField(entity, parts.First());

        foreach (var part in parts.Skip(1))
        {
            if (propertyInfo == null)
                throw new ArgumentException($"Invalid sorting option - {field}.");

            if (typeof(BaseDomain).IsAssignableFrom(propertyInfo.PropertyType))
            {
                if (propertyInfo.PropertyType.GetProperty(part) != null)
                {
                    property = Expression.PropertyOrField(property, part);
                    propertyInfo = propertyInfo.PropertyType.GetProperty(part);
                }
                else
                    throw new ArgumentException($"Invalid sorting option - {field}.");
            }
        }

        return property;
    }

    private static IOrderedQueryable<TModel> AddOrder<TModel, TProperty>(IQueryable<TModel> query, Expression<Func<TModel, TProperty>> orderBy, bool desc = false) =>
        desc ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

    private static IOrderedQueryable<TModel> AddThenOrder<TModel, TProperty>(IOrderedQueryable<TModel> query, Expression<Func<TModel, TProperty>> orderBy, bool desc = false) =>
        desc ? query.ThenByDescending(orderBy) : query.ThenBy(orderBy);
}