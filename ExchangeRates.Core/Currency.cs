using System.Runtime.Serialization;

namespace ExchangeRates
{
    /// <summary>
    /// Para birimi.
    /// </summary> 
    [DataContract]
    public enum Currency : ushort
    {
        /// <summary>
        /// Varsayılan değerdir. Kodda <c>default</c> ile de ifade edilebilir. String gösterimi <c>null</c>dır. 
        /// </summary> 
        [EnumMember] NULL = default,
        /// <summary>
        /// TL
        /// </summary>
        [EnumMember] TRY = 949,
        /// <summary>
        ///ABD DOLARI
        /// </summary>
        [EnumMember] USD = 840,
        /// <summary>
        /// EURO
        /// </summary>
        [EnumMember] EUR = 978,
        /// <summary>
        /// İNGİLİZ STERLİNİ
        /// </summary>
        [EnumMember] GBP = 826,
        /// <summary>
        /// AVUSTRALYA DOLARI
        /// </summary>
        [EnumMember] AUD = 36,
        /// <summary>
        /// DANİMARKA KRONU
        /// </summary>
        [EnumMember] DKK = 208,
        /// <summary>
        /// İSVİÇRE FRANGI
        /// </summary>
        [EnumMember] CHF = 756,
        /// <summary>
        /// İSVEÇ KRONU
        /// </summary>
        [EnumMember] SEK = 752,
        /// <summary>
        /// KANADA DOLARI
        /// </summary>
        [EnumMember] CAD = 124,
        /// <summary>
        /// KUVEYT DİNARI
        /// </summary>
        [EnumMember] KWD = 414,
        /// <summary>
        /// NORVEÇ KRONU
        /// </summary>
        [EnumMember] NOK = 578,
        /// <summary>
        /// SUUDİ ARABİSTAN RİYALİ
        /// </summary>
        [EnumMember] SAR = 682,
        /// <summary>
        /// JAPON YENİ
        /// </summary>
        [EnumMember] JPY = 392,
        /// <summary>
        ///BULGAR LEVASI
        /// </summary>
        [EnumMember] BGN = 975,
        /// <summary>
        /// RUMEN LEYİ
        /// </summary>
        [EnumMember] RON = 946,
        /// <summary>
        /// RUS RUBLESİ
        /// </summary>
        [EnumMember] RUB = 643,
        /// <summary>
        /// İRAN RİYALİ
        /// </summary>
        [EnumMember] IRR = 364,
        /// <summary>
        /// ÇİN YUANI
        /// </summary>
        [EnumMember] CNY = 156,
        /// <summary>
        /// PAKİSTAN RUPİSİ
        /// </summary>
        [EnumMember] PKR = 586,
        /// <summary>
        /// KATAR RİYALİ
        /// </summary>
        [EnumMember] QAR = 634,
    }


}
