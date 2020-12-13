namespace ExchangeRates.Core.Export
{
    public class CsvExportResult : ExportResult<string>
    {
        public override ExportFileType FileType => ExportFileType.Csv;
    }

}
