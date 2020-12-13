using System.Collections.Generic;

namespace ExchangeRates.TcmbProvider
{
    public class SearchResponse
    {

        /// <summary>
        /// Arama sonu oluşan kur listesi
        /// </summary>
        public IEnumerable<TcmbExchangeRate> Items { get; set; }

        /// <summary>
        /// Sıralama yapılan  özellik bilgisi
        /// </summary>
        public OrderBy OrderBy { get; set; }

        /// <summary>
        /// Sıralama tipi
        /// </summary>
        public OrderByType OrderByType { get; set; }
    }
}
