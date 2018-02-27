using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class ReflectionUtil
{
    public static Type GetAbilityTypeFromName(string TypeName)
    {
        //HACK: hardcoced namespace
        return System.Reflection.Assembly.GetExecutingAssembly().GetType("Powerups." + TypeName);
    }
}
