using System.ComponentModel;
using System.Reflection;

namespace InSynq.Common.Extensions;

public static class EnumExtensions
{
    public static bool In<T>(this T value, params T[] args) where T : struct, Enum => args?.Contains(value) ?? false;

    public static List<T> GetEnumList<T>(this string enumsString) where T : struct, Enum
    {
        if (!enumsString.HasValue())
            return [];

        return enumsString.Split(',')
            .Select(e => Enum.TryParse<T>(e.Trim(), out var parsedEnum) ? parsedEnum : (T?)null)
            .Where(parsedEnum => parsedEnum.HasValue)
            .Select(parsedEnum => parsedEnum.Value)
            .ToList();
    }

    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        return Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is not DescriptionAttribute attribute
            ? value.ToString()
            : attribute.Description;
    }

    public static string[] GetEnumNames<T>(this IEnumerable<int> enums) where T : struct, Enum
        => enums.Any() ? enums.Select(_ => ((T)(object)_).ToString()).ToArray() : [];
}