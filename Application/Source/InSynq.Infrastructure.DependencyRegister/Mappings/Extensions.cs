using AutoMapper;
using InSynq.Core.Dtos;
using InSynq.Core.Extensions;
using System.Linq.Expressions;

namespace InSynq.Infrastructure.DependencyRegister.Mappings;

public static class Extensions
{
    public static IMappingExpression<TSource, TDestination> ForLookup<TSource, TDestination, TEnum>(this IMappingExpression<TSource, TDestination> source,
      Expression<Func<TDestination, LookupValueDto>> lookupExpression,
      Func<TSource, TEnum?> sourceMappingExpression
      )
    where TEnum : struct, Enum =>
        source.ForMember(lookupExpression, opt => opt.MapFrom((src, dest, prop, ctx) => sourceMappingExpression(src).ToLookupValueDto()));

    public static IMappingExpression<TSource, TDestination> ForLookup<TSource, TDestination, TEnum>(this IMappingExpression<TSource, TDestination> source,
      Expression<Func<TDestination, LookupValueDto>> lookupExpression,
      Func<TSource, TEnum> sourceMappingExpression
      )
    where TEnum : struct, Enum =>
        source.ForMember(lookupExpression, opt => opt.MapFrom((src, dest, prop, ctx) => sourceMappingExpression(src).ToLookupValueDto()));
}