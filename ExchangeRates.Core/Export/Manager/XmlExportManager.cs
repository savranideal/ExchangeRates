using System;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Export
{
    public class XmlExportManager : ExportManager
    {

        public Task<XmlExportResult> ExportAsync<TInput>(TInput input) where TInput : class 
        {

            var t = new XmlExportResult();
            try
            {
                t.Data = Xml.Serialize(input);
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
