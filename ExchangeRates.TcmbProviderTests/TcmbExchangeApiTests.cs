using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using ExchangeRates.TcmbProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading;
using ExchangeRates.Core.Export;
using ExchangeRates.Core;

namespace ExchangeRates.TcmbProvider.Tests
{
    [TestClass()]
    [TestFixture()]
    public class TcmbExchangeApiTests
    {

        
        [Test()]
        [TestCase(Currency.USD)]
        [TestCase(Currency.EUR)] 
        public async Task TCMB_Api_Single_Search(Currency currency)
        {
            TcmbExchangeApi api = new TcmbExchangeApi();
            var response = await api.SearchAsync(new SearchRequest
            { 
                Currencies = new List<Currency> { currency}
            });
            
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Items);
            Assert.IsNotNull(response.Items.SingleOrDefault(c=>c.Currency==currency));

        } 
        [Test()]
        [TestCase(Currency.USD, Currency.EUR, Currency.GBP)] 
        public async Task TCMB_Api_Multiple_Search(Currency usd,Currency eur,Currency gbp)
        {
            TcmbExchangeApi api = new TcmbExchangeApi();
            var currencies = new List<Currency> { usd, eur, gbp };
            var response = await api.SearchAsync(new SearchRequest
            { 
                Currencies = currencies
            });
            
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Items);
            Assert.IsNotNull(response.Items.Count()==currencies.Count());
            Assert.IsNotNull(response.Items.All(c=>currencies.Contains(c.Currency)));

        } 

        [Test()]
        [TestCase(Currency.USD)] 
        public async Task TCMB_Api_Get_USD_ForexBuying_Rate_Is_79349(Currency currency)
        {
            TcmbExchangeApi api = new TcmbExchangeApi();
            var response = await api.SearchAsync(new SearchRequest
            { 
                Currencies = new List<Currency> { currency}
            });
            
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Items);
            Assert.IsNotNull(response.Items.SingleOrDefault(c=>c.Currency==currency));
            var rate = 7.9349m;
            Assert.That(response.Items.SingleOrDefault(c=>c.Currency==currency).ForexBuying==rate);


        } 
    

        [Test()]
        [TestCase(OrderBy.ForexBuyingDesc)] 
        public async Task TCMB_Api_OrderBy_ForexBuying_Desc_IsFirst_KWD_Success(OrderBy orderBy)
        {
            TcmbExchangeApi api = new TcmbExchangeApi();
            var response = await api.SearchAsync(new SearchRequest
            {  
                OrderBy=orderBy
            });
            
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Items); 
            Assert.That(response.Items.FirstOrDefault().Currency==Currency.KWD); 
        } 
       
    }
}