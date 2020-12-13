namespace ExchangeRates.Core.Export
{
    public class ExcelExportResult : ExportResult<byte[]>
    {
        public override ExportFileType FileType => ExportFileType.Excel;
    }

}
