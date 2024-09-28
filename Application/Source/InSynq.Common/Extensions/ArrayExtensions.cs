namespace InSynq.Common.Extensions;

public static class ArrayExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => source == null || !source.Any();

    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> source) => !source.IsNullOrEmpty();

    public static IEnumerable<T> IfNotNull<T>(this IEnumerable<T> source) => source ?? [];

    public static IEnumerable<T> IfNotNullOrEmpty<T>(this IEnumerable<T> source) => source != null && source.Any() ? source : [];

    public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var result = -1;
        var num = 0;

        foreach (var item in source)
        {
            if (predicate(item))
            {
                result = num;
                break;
            }

            num++;
        }

        return result;
    }
}