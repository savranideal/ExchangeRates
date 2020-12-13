using System;

namespace ExchangeRates.TcmbProvider
{
    /// <summary>
    /// Sıralaam yı belirler
    /// </summary>
    [Flags]
    public enum OrderBy
    {
        CurrencyAsc =           1 << 0,
        CurrencyDesc =          1 << 1,
        ForexBuyingAsc =        1 << 3,
        ForexBuyingDesc =       1 << 4,
        ForexSellingAsc =       1 << 5,
        ForexSellingDesc =      1 << 6,
        BanknoteBuyingAsc =     1 << 7,
        BanknoteBuyingDesc =    1 << 8,
        BanknoteSellingAsc =    1 << 9,
        BanknoteSellingDesc =   1 << 10,

    }
}
