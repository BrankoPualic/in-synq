namespace InSynq.Core.Model.Models;

public abstract class AuditIndexDomain<T, TKey> : AuditDomain<TKey>
    where TKey : struct
    where T : AuditIndexDomain<T, TKey>
{
    private static readonly Lazy<string> _tableName = new(ModelExtensions.GetFullTableName<T>);

    public static string TableName => _tableName.Value;

    protected class DatabaseIndex(string indexName) : Model.DatabaseIndex(indexName, TableName)
    { }
}