using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ExchangeRates.TcmbProvider
{
    public class SearchRequest
    {
        /// <summary>
        /// Para birimine göre filtreler
        /// </summary>
        public IEnumerable<Currency> Currencies { get; set; }

        /// <summary>
        /// Sıralama yapılacak özellik bilgisi
        /// </summary>
        public OrderBy OrderBy { get; set; }

        /// <summary>
        /// Sıralama tipi
        /// </summary>
        public OrderByType OrderByType { get; set; }

    }
}
