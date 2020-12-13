using System.Collections.Generic;

namespace ExchangeRates
{
    /// <summary>
    /// Kur dönüşümü yapabilen provider arayüzü
    /// </summary>
    public interface IExchangeProvider<TModel> where TModel :ExchangeRate
    {
        /// <summary>
        /// Eğer exchangeProvider init olması gerekiyorsa burada tanımlanmalı.
        /// </summary>
        void Initialize(bool waitForFinish = false);

        /// <summary>
        /// <paramref name="currency"/> ye ait  kur bilgilerini döner.
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        TModel GetExchangeRate(Currency currency);

        /// <summary>
        /// Tüm kur bilgilerini döner.
        /// </summary> 
        /// <returns></returns>
        IDictionary<Currency,TModel> GetExchangeRates();

    }


}
