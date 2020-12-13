using System;

namespace ExchangeRates
{
    /// <summary>
    /// Currency yardımcı metodları
    /// </summary>
    public static class CurrencyExtensions
    {   
        /// <summary>
        /// Sistemde tanımlı currency kodunu string olarak döner. <see cref="Enum.ToString"/>'den daha hızlıdır. Eğer <see cref="Currency.NULL"/> ya da <see cref="default"/> ise <c>null</c> döner.
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToCodeString(this Currency currency)
        {
            return Constants.CurrencyCodeStrings[(ushort)currency];
        }

        /// <summary>
        /// Currency eğer default değere yani <see cref="Currency.NULL"/> değerine sahipse <paramref name="defaultCurrency"/> ile belirtilen parametreyi dönderir, aksi halde kendi değerini dönderir.
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="defaultCurrency"></param>
        /// <returns></returns>
        public static Currency OrDefault(this Currency currency, Currency defaultCurrency)
        {
            return currency != default ? currency : defaultCurrency;
        }
    }


}
