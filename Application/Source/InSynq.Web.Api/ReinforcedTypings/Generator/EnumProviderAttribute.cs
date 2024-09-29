namespace InSynq.Web.Api.ReinforcedTypings.Generator;

[AttributeUsage(AttributeTargets.Field)]
public class EnumProviderAttribute<T> : EnumProviderAttribute
{
    public EnumProviderAttribute() : base(typeof(T))
    {
    }
}

public abstract class EnumProviderAttribute : Attribute
{
    public Type EnumType { get; }

    public EnumProviderAttribute(Type enumType) => EnumType = enumType;
}

//public class EnumProviderAttribute : TsFunctionAttribute
//{
//    public Type EnumType { get; }

//    public EnumProviderAttribute(Type enumType)
//    {
//        EnumType = enumType;
//    }
//}