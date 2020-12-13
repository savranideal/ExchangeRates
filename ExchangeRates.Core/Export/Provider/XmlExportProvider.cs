using System.Threading.Tasks;

namespace ExchangeRates.Core.Export
{
    public class XmlExportProvider : IXmlExportProvider
    {
        public Task<XmlExportResult> Export(object input)
        {

            var exportResult = new XmlExportResult()
            {
                MimeType = "text/xml",

            };
            try
            {
                var data = Xml.Serialize(input);
                exportResult.Data = data;
                exportResult.Succeeded = true;
            }
            catch (System.Exception e)
            {
                exportResult.Succeeded = false;
            }

            return Task.FromResult(exportResult);
        }

    }
}
