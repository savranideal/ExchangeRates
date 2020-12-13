using System;
using System.Collections.Generic;

namespace ExchangeRates
{
    public static class EnumExtensions
    {


        /// <summary>
        /// Flag tipindeki bir enum'ın içerdiği değerleri döner.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IEnumerable<Enum> GetFlags(this Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value;
        }
    }
}
