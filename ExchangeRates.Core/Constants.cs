using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ExchangeRates
{
    public static partial class Constants
    {
        /// <summary>
        /// Currency ISO 4217 kodlarına karşılık <see cref="Currency"/> enum değerleri. Eğer bunu gösterim haricinde kullanıyorsanız (mesela string'den currency'ye çevirmek için) kullanmayın! Bunun yerine modelinizde string yerine Currency kullanın.
        /// </summary>
        public static readonly ReadOnlyDictionary<string, Currency> CurrencyFromIsoCode = new ReadOnlyDictionary<string, Currency>(((Currency[])Enum.GetValues(typeof(Currency)))
            .Where(c => c != Currency.NULL)
            .ToDictionary(c => c.ToString(), c => c));
        public static readonly IReadOnlyList<string> CurrencyCodeStrings = Enumerable.Range(0, Enum.GetValues(typeof(Currency)).Cast<ushort>().Max() + 1)
            .Select(i => i == default ?
                string.Empty :
                Enum.GetName(typeof(Currency), (ushort)i))
            .ToArray();

        /// <summary>
        /// Kur takma adları ile Iso kodları eşleştirmesi
        /// </summary>
        public static readonly ReadOnlyDictionary<string, Currency> CurrencyAliases =
            new ReadOnlyDictionary<string, Currency>(new Dictionary<string, Currency>
            {
                ["₺"] = Currency.TRY,
                ["TL"] = Currency.TRY,
                ["$"] = Currency.USD,
                ["€"] = Currency.EUR,
                ["£"] = Currency.GBP
            });

        /// <summary>
        /// Kur sembolleri.
        /// </summary>
        public static readonly ReadOnlyDictionary<Currency, string> CurrencySymbols =
            new ReadOnlyDictionary<Currency, string>(new Dictionary<Currency, string>
            {
                [Currency.TRY] = "₺",
                [Currency.USD] = "$",
                [Currency.EUR] = "€",
                [Currency.GBP] = "£"
            });
    }


}
