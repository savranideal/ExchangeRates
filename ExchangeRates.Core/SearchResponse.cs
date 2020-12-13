using System.Collections.Generic;
using System.Xml.Serialization;

namespace ExchangeRates 
{
    public abstract class SearchResponse<TModel> where TModel : class

    {

        /// <summary>
        /// Arama sonucu oluşan sonuç listesi
        /// </summary> 
        public List<TModel> Items { get; set; }
         
    }
}
