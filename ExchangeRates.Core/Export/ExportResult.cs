namespace ExchangeRates.Core.Export
{

    public abstract class ExportResult
    {
        /// <summary>
        /// Export işleminin başarılı olup olmadığı bilgisi
        /// </summary> 
        public bool Succeeded { get; set; }
        /// <summary>
        /// Export işlemi başarısız ise dönen hata mesajı
        /// </summary> 
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Export işlemi sonucu dönen dosyanın tipi <see cref="ExportFileType"/>
        /// </summary> 
        public abstract ExportFileType FileType { get; }
        /// <summary>
        /// Export işlemi sonucu dönen dosyanın MIME tipi
        /// </summary> 
        public string MimeType { get; set; }
         
    }
    public abstract class ExportResult<TOutput>:ExportResult where TOutput :class
    { 

        /// <summary>
        /// Export işlemi sonucu oluşan dosyanın indirilmek üzere hazırlanmış <see cref="TOutput"/> tipinde  değeri
        /// </summary> 
        public TOutput Data { get; set; }
    }



}
