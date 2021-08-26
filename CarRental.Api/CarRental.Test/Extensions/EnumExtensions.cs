using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.Test.Extensions
{
    public class EnumExtensions<T> where T : Enum 
    {
        public static IEnumerable<object[]> ExcludeEnum(T[] excludedEnums)
        {
            foreach (var enumValue in Enum.GetValues(typeof(T)))
            {
                if (!excludedEnums.Contains((T)enumValue))
                {
                    yield return new object[] { enumValue };
                }
            }
        }
    }
}
