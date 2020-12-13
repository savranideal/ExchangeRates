using System;
using System.Diagnostics;

namespace ExchangeRates
{
    /// <summary>
    /// Kur bilgisi
    /// </summary>
    [Serializable] 
    [DebuggerDisplay("{Currency} {Value}")]
    public class ExchangeRate
    { 
        /// <summary>
        /// Kur ismi
        /// </summary>
        public Currency Currency { get; set; }


        /// <summary>
        /// Kur değeri
        /// </summary> 
        public decimal Value { get; set; }
    }


}
