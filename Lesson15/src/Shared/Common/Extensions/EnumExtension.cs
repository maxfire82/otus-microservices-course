using System.Reflection;
using System.Runtime.Serialization;

namespace Common.Extensions;

public static class EnumExtension
{
    public static string ToEnumMemberAttrValue(this Enum @enum)
    {
        var attr = 
            @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.
                GetCustomAttributes(false).OfType<EnumMemberAttribute>().
                FirstOrDefault();
        if (attr == null)
            return @enum.ToString();
        return attr.Value;
    }
}