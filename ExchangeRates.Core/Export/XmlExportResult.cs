namespace ExchangeRates.Core.Export
{
    public class XmlExportResult : ExportResult<string>
    {
        public override ExportFileType FileType => ExportFileType.Xml;
    }

}
