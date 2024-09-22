namespace InSynq.Common.Extensions;

public static class ArrayExtensions
{
	public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => source == null || !source.Any();
}