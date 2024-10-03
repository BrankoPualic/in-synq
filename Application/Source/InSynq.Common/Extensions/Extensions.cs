namespace InSynq.Common.Extensions;

public static class Extensions
{
    public static string ToFullTextSearch(this string filter) =>
        filter.IsNullOrWhiteSpace()
            ? null
            : $"{filter.Replace("\"", string.Empty).Trim().Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Select(_ => $"\"{_}*\"").Join(" AND ")}";

    public static bool ToFullTextSearch(this string filter, out string search)
    {
        search = filter.ToFullTextSearch();
        return search.IsNotNullOrWhiteSpace();
    }
}