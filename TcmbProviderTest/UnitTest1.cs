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
                OrderBy = OrderBy.Currency | OrderBy.ForexBuying,
                OrderByType = OrderByType.Asc
            });


            var k = response;

        }
    }
}
