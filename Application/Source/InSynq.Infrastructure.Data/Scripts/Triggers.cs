namespace InSynq.Infrastructure.Data.Scripts;

internal class Triggers
{
	internal static string Audit<T>() => Audit(ModelExtensions.GetFullTableName<T>(), ModelExtensions.GetAuditExcludeColumns<T>());

	internal static string Audit(string tableName, string? excludeColumns = null) => $"EXEC [dbo].[usp_CreateAuditTrigger] '{tableName}'{excludeColumns.IfNotNullOrWhiteSpace(", '{0}'")}";
}