using System.Collections.Concurrent;

namespace ExchangeRates
{
    /// <summary>
    /// Currency bazlı kur bilgisi tutar. Key değeri currency'dir.
    /// </summary> 
    public class ExchangeRates<TModel> : ConcurrentDictionary<Currency, TModel>
    {


    }


}
