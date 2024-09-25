namespace InSynq.Core.Service.Extensions;

public static class AutoMapperExtensions
{
	public static TDestination To<TDestination>(this IMapper mapper, object source) => mapper.Map<TDestination>(source);

	public static IEnumerable<TDestination> To<TDestination>(this IMapper mapper, IEnumerable<object> source) => mapper.Map<IEnumerable<TDestination>>(source);
}