namespace ExchangeRates.Core.Export
{
    public class JsonExportResult : ExportResult<string>
    {
        public override ExportFileType FileType => ExportFileType.Json;
    }
    public class PdfExportResult : ExportResult<byte[]>
    {
        public override ExportFileType FileType => ExportFileType.Pdf;
    }
}
