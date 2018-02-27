using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class ReflectionUtil
{
    public static Type GetAbilityTypeFromName(string TypeName)
    {
        //HACK hardcoded namespace
        return System.Reflection.Assembly.GetExecutingAssembly().GetType("Powerups." + TypeName);
    }

    public static string GetTypeName(Type type)
    {
        return type.Name;
    }
    public static string GetTypeName(Object obj)
    {
        return obj.GetType().Name;
    }
}
