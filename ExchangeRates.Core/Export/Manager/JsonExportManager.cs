using System;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Export
{
    public class JsonExportManager : ExportManager
    {

        public Task<JsonExportResult> ExportAsync<TInput>(TInput input) where TInput : class  
        {

            var t = new JsonExportResult();
            try
            {
                t.Data = Json.Serialize(input);
                t.Succeeded = true;
            }
            catch (Exception e)
            {
                t.ErrorMessage = e.Message;
                t.Succeeded = false;
            }

            return Task.FromResult(t);

        }

    }
}
