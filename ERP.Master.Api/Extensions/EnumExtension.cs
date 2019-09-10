using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ERP.Master.Api.Attributes;

/// <summary>
/// Classe extensiva do enum.
/// </summary>
public static class EnumExtension
{
    /// <summary>
    /// ToString.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string Texto(this Enum value)
    {
        // Get the type
        Type type = value.GetType();

        // Get fieldinfo for this type
        FieldInfo fieldInfo = type.GetField(value.ToString());

        // Get the stringvalue attributes
        if (fieldInfo != null)
        {
            EnumValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(EnumValueAttribute), false) as EnumValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].Value : null;
        }
        else return "";
    }
}
