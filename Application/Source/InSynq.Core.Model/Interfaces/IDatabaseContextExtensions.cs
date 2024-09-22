using System.Linq.Expressions;

namespace InSynq.Core.Model;

public static class IDatabaseContextExtensions
{
	public static void Create<TModel>(this IDatabaseContext db, TModel model) where TModel : class
	{
		db.Set<TModel>().Add(model);
		db.AuditCreate(model);
	}

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

	// private

	private static void AuditCreate<TModel>(this IDatabaseContextBase db, TModel model) where TModel : class
	{
		var auditType = GetAuditType(db, model);

		var now = DateTime.UtcNow;

		var auditEntity = (IAuditDomain<long>)Activator.CreateInstance(auditType);

		auditEntity.AssignProperties(model);
		auditEntity.LogType = Constants.AUDIT_LOG_TYPE_INSERT;
		auditEntity.AuditTimeStamp = now;

		db.Entry(auditEntity).State = EntityState.Added;
	}

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
}