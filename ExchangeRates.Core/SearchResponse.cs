using System.Collections.Generic;

namespace ExchangeRates 
{
    public abstract class SearchResponse<TModel> where TModel : class

    {

        /// <summary>
        /// Arama sonucu oluşan sonuç listesi
        /// </summary>
        public IEnumerable<TModel> Items { get; set; }
         
    }
}
