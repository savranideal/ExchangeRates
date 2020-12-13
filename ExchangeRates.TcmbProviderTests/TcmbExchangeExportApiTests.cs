using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;
using System.Linq;
using ExchangeRates.Core;

namespace ExchangeRates.TcmbProvider.Tests
{
    [TestClass()]
    [TestFixture()]
    public class TcmbExchangeExportApiTests
    {



        [Test()]
        public async Task Export_Json()
        {
            TcmbExchangeApi api = new TcmbExchangeApi();
            var exportApi = new TcmbExchangeExportApi(api);
            var response = await exportApi.ToJsonAsync(new SearchRequest
            {
            });

            Assert.IsNotNull(response);
            Assert.That(response.FileType == Core.Export.ExportFileType.Json);
            Assert.IsNotEmpty(response.Data);

        }

        [Test()]
        [TestCase(Currency.USD, Currency.EUR, Currency.GBP)]
        public async Task Export_Json_Filtered(Currency usd, Currency eur, Currency gbp)
        {
            TcmbExchangeApi api = new TcmbExchangeApi();
            var exportApi = new TcmbExchangeExportApi(api);
            var currencies = new List<Currency> { usd, eur, gbp };
            var response = await exportApi.ToJsonAsync(new SearchRequest
            {
                Currencies = currencies,
            });

            Assert.IsNotNull(response);
            Assert.That(response.FileType == Core.Export.ExportFileType.Json);
            Assert.IsNotEmpty(response.Data);
            var jsonData=Json.Deserialize<IEnumerable<TcmbExchangeRate>>(response.Data);
            Assert.IsNotNull(jsonData);
            Assert.IsNotNull(jsonData.All(c => currencies.Contains(c.Currency)));
        }



        [Test()]
        public async Task Export_Xml()
        {
            TcmbExchangeApi api = new TcmbExchangeApi();
            var exportApi = new TcmbExchangeExportApi(api);
            var response = await exportApi.ToXmlAsync(new SearchRequest
            {
            });

            Assert.IsNotNull(response);
            Assert.That(response.FileType == Core.Export.ExportFileType.Xml);
            Assert.IsNotEmpty(response.Data);

        }

        [Test()]
        [TestCase(Currency.USD, Currency.EUR, Currency.GBP)]
        public async Task Export_Xml_Filtered(Currency usd, Currency eur, Currency gbp)
        {
            TcmbExchangeApi api = new TcmbExchangeApi();
            var exportApi = new TcmbExchangeExportApi(api);
            var currencies = new List<Currency> { usd, eur, gbp };
            var response = await exportApi.ToXmlAsync(new SearchRequest
            {
                Currencies = currencies,
            });

            Assert.IsNotNull(response);
            Assert.That(response.FileType == Core.Export.ExportFileType.Xml);
            Assert.IsNotEmpty(response.Data);
            var jsonData = Xml.Deserialize<IEnumerable<TcmbExchangeRate>>(response.Data);
            Assert.IsNotNull(jsonData);
            Assert.IsNotNull(jsonData.All(c => currencies.Contains(c.Currency)));
        }


        [Test()]
        public async Task Export_Csv()
        {
            TcmbExchangeApi api = new TcmbExchangeApi();
            var exportApi = new TcmbExchangeExportApi(api);
            var response = await exportApi.ToCsvAsync(new SearchRequest
            {
            });

            Assert.IsNotNull(response);
            Assert.That(response.FileType == Core.Export.ExportFileType.Csv);
            Assert.IsNotEmpty(response.Data);

        }
    }
}