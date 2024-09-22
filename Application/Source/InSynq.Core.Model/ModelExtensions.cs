using InSynq.Common.Extensions;
using InSynq.Core.Model.Attributes;
using InSynq.Core.Model.Models;
using System.Reflection;

namespace InSynq.Core.Model;

public static class ModelExtensions
{
	public static string GetTableName(this Type type) => (Attribute.GetCustomAttribute(type, typeof(TableAttribute)) as TableAttribute)?.Name ?? type.Name;

	public static string GetFullTableName<TDatabaseEntity>()
	{
		var type = typeof(TDatabaseEntity);
		var attr = Attribute.GetCustomAttribute(type, typeof(TableAttribute)) as TableAttribute;
		return (attr?.Name) == null
			? type.Name
			: new[] { attr.Schema, attr.Name }!.Join(".");
	}

	public static string GetAuditExcludeColumns<TDatabaseEntity>()
	{
		var columns = typeof(TDatabaseEntity).GetProperties()
			.Where(_ => _.GetCustomAttribute<AuditExcludeAttribute>() != null)
			.Select(_ => _.Name).ToList();
		return columns.IsNullOrEmpty()
			? null
			: columns.SerializeJsonObject();
	}

	public static void AssignProperties(this object target, object source)
	{
		var sourceType = source.GetType();
		var targetType = target.GetType();

		foreach (var propertyInfo in sourceType.GetProperties().Where(_ => _.CanRead && _.CanWrite))
		{
			var targetProperty = targetType.GetProperty(propertyInfo.Name);
			if (targetProperty != null && !typeof(BaseDomain).IsAssignableFrom(propertyInfo.PropertyType))
			{
				targetProperty.SetValue(target, propertyInfo.GetValue(source, null), null);
			}
		}
	}
}