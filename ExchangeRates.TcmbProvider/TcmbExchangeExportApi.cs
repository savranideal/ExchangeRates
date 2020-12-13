using ExchangeRates.Core.Export;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.TcmbProvider
{
    public class TcmbExchangeExportApi
    {

        public TcmbExchangeApi ExchangeApi { get; }

        public TcmbExchangeExportApi(TcmbExchangeApi exchangeApi)
        {
            ExchangeApi = exchangeApi;
        }

        /// <summary>
        /// Json export işlemi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<JsonExportResult> ToJsonAsync(SearchRequest request)
        {
            var searchResult=await ExchangeApi.SearchAsync(request);
            if(searchResult==null || searchResult.Items == null)
            {
                return new JsonExportResult { ErrorMessage="Search result is empty"};
            }
            var exportManager = ExportFactory.CreateJsonExportManager();
            var exportedData = await exportManager.ExportAsync(searchResult.Items);
            return exportedData;
        }

        /// <summary>
        /// CSV export işlemi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CsvExportResult> ToCsvAsync(SearchRequest request)
        {
            var searchResult=await ExchangeApi.SearchAsync(request);
            if(searchResult==null || searchResult.Items == null)
            {
                return new CsvExportResult { ErrorMessage="Search result is empty"};
            }
            var exportManager = ExportFactory.CreateCsvExportManager();
            var exportedData = await exportManager.ExportAsync(searchResult.Items);
            return exportedData;
        }

        /// <summary>
        /// XML export işlemi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<XmlExportResult> ToXmlAsync(SearchRequest request)
        {
            var searchResult=await ExchangeApi.SearchAsync(request);
            if(searchResult==null || searchResult.Items == null)
            {
                return new XmlExportResult { ErrorMessage="Search result is empty"};
            }
            var exportManager = ExportFactory.CreateXmlExportManager(); 
            var exportData=await exportManager.ExportAsync(searchResult.Items);
            return exportData;
        }

    }
}
