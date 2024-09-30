namespace InSynq.Core.Model.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class BgColorAttribute : Attribute
{
    public string BgColor { get; }

    public BgColorAttribute(string color)
    {
        BgColor = color;
    }
}