using ExchangeRates;
using ExchangeRates.TcmbProvider;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TcmbProviderTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            TcmbExchangeApi api = new TcmbExchangeApi();
          var response=  await api.SearchAsync(new SearchRequest
            { 
                OrderBy = OrderBy.CurrencyAsc | OrderBy.ForexBuyingDesc,
                OrderByType = OrderByType.Asc,
                Currencies=Currency.USD
            });


            var k = response;

        }
    }
}
