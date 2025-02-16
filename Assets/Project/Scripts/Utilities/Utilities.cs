using System;
using System.Collections;
using System.Collections.Generic;

public static class Utilities 
{
    public static string ConvertEnumToString<T>(T enumValue) where T : System.Enum
    {
        return enumValue.ToString();
    }

    public static int ConvertFloatToTimeUniTask(float time) => (int)time * 1000;
    
    public static TimeSpan ConvertFloatToTimeSpan(float time) => TimeSpan.FromSeconds(time);
}
