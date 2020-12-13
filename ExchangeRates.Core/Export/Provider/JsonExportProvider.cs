using System.Threading.Tasks;

namespace ExchangeRates.Core.Export
{
    public class JsonExportProvider : IJsonExportProvider
    {
        public Task<JsonExportResult> Export(object input)
        {
            var exportResult = new JsonExportResult()
            {
                MimeType = "application/json",

            };
            try
            {
                var data = Json.Serialize(input,indent:true);
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
