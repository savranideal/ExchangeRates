using ExchangeRates.Core.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.TcmbProvider
{
    public class TcmbExchangeApi : IExportProvider<SearchRequest, XmlExportResult>

    ///,IXmlExportProvider => şeklinde de kullanılaiblir veya dire <see cref="XmlExportProvider"> inject edilerek</see>  kullanılabilir

    { 
        public Task<SearchResponse> SearchAsync(SearchRequest request)
        {
            var tcmbExchangeRateProvider = new TcmbExchangeRateProvider();
            tcmbExchangeRateProvider.Initialize(true);
            var response = new SearchResponse();
            var filteredItem = new List<TcmbExchangeRate>();
            var alldata = tcmbExchangeRateProvider.GetExchangeRates();
            if (request.Currency.HasValue && alldata.ContainsKey(request.Currency.Value))
                filteredItem.Add(alldata[request.Currency.Value]);
            else 
                filteredItem.AddRange(alldata.Values);
            
            IEnumerable<TcmbExchangeRate> items = filteredItem;
            var orderbys = request.OrderBy.GetFlags().Cast<OrderBy>();

            var isAsc = request.OrderByType == OrderByType.Asc;
            IOrderedEnumerable<TcmbExchangeRate> orderedQuery = null;
            foreach (var orderby in orderbys)
            {

                switch (orderby)
                {
                    case OrderBy.Currency:
                        if (orderedQuery != null)
                            orderedQuery = isAsc ? orderedQuery.ThenBy(c => c.Currency) : orderedQuery.ThenByDescending(c => c.Currency);
                        else
                        {
                            orderedQuery = isAsc ? items.OrderBy(c => c.Currency) : items.OrderByDescending(c => c.Currency);
                        }
                        break;
                    case OrderBy.ForexBuying:
                        if (orderedQuery != null)
                            orderedQuery = isAsc ? orderedQuery.ThenBy(c => c.ForexBuying) : orderedQuery.ThenByDescending(c => c.ForexBuying);
                        else
                        {
                            orderedQuery = isAsc ? items.OrderBy(c => c.ForexBuying) : items.OrderByDescending(c => c.ForexBuying);
                        }
                        break;
                    case OrderBy.ForexSelling:
                        if (orderedQuery != null)
                            orderedQuery = isAsc ? orderedQuery.ThenBy(c => c.ForexSelling) : orderedQuery.ThenByDescending(c => c.ForexSelling);
                        else
                            orderedQuery = isAsc ? items.OrderBy(c => c.ForexSelling) : items.OrderByDescending(c => c.ForexSelling);

                        break;
                    case OrderBy.BanknoteBuying:
                        if (orderedQuery != null)
                            orderedQuery = isAsc ? orderedQuery.ThenBy(c => c.BanknoteBuying) : orderedQuery.ThenByDescending(c => c.BanknoteBuying);
                        else
                            orderedQuery = isAsc ? items.OrderBy(c => c.BanknoteBuying) : items.OrderByDescending(c => c.BanknoteBuying);
                        break;
                    case OrderBy.BanknoteSelling:
                        if (orderedQuery != null)
                            orderedQuery = isAsc ? orderedQuery.ThenBy(c => c.BanknoteSelling) : orderedQuery.ThenByDescending(c => c.BanknoteSelling);
                        else
                            orderedQuery = isAsc ? items.OrderBy(c => c.BanknoteSelling) : items.OrderByDescending(c => c.BanknoteSelling);
                        break;
                    default:
                        break;
                }
            } 
            response.OrderBy = request.OrderBy;
            response.OrderByType = request.OrderByType;
            response.Items = orderedQuery.AsEnumerable();
            return  Task.FromResult(response);
        }

        public Task<XmlExportResult> Export(SearchRequest input)
        {
            throw new NotImplementedException();
        }

    }
}
