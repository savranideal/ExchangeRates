namespace ExchangeRates.TcmbProvider
{
    /// <summary>
    /// TCMB Kur Modeli
    /// </summary>
    public class TcmbExchangeRate : ExchangeRate
    {

        /// <summary>
        /// Döviz Alış
        /// </summary>
        public decimal ForexBuying { get; set; }

        /// <summary>
        /// Döviz Satış
        /// </summary>
        public decimal ForexSelling { get; set; }

        /// <summary>
        /// Efektif Alış
        /// </summary>
        public decimal BanknoteBuying { get; set; }

        /// <summary>
        /// Efektif Satış
        /// </summary>
        public decimal BanknoteSelling { get; set; }


        /// <summary>
        /// Dolar çağraz kur değeri
        /// </summary>
        public decimal? CrossRateUSD { get; set; }

        /// <summary>
        /// Dönüşümdeki sıra
        /// </summary>
        public int? Order { get; set; }
    }



}
