using Newtonsoft.Json.Linq;

namespace ExchangeRates.TcmbProvider
{
    public class SearchRequest
    {
        /// <summary>
        /// Para birimine göre filtreler
        /// </summary>
        public Currency? Currency { get; set; }

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
