using System.Collections.Generic;

namespace ExchangeRates.TcmbProvider
{
    public class TcmbSearchResponse: SearchResponse<TcmbExchangeRate>
    { 

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
