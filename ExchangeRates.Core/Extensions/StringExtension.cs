using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExchangeRates
{
    public static class StringExtension
    {
        /// <summary>
        /// <see cref="decimal.TryParse(ReadOnlySpan{char}, out decimal)"/> metodu aracılığı ile decimal dönüşümü yapar
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string value)
        {
              if (decimal.TryParse(value.AsSpan(),System.Globalization.NumberStyles.Currency, System.Globalization.CultureInfo.CurrentCulture, out decimal decimalValue))
                return decimalValue;

            return default;

        }
         

    }
}
