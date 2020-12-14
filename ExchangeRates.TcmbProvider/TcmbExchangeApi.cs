using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.TcmbProvider
{
    public class TcmbExchangeApi : ISearchable<SearchRequest, TcmbSearchResponse, TcmbExchangeRate>
    {
        public Task<TcmbSearchResponse> SearchAsync(SearchRequest request)
        {
        /// Bu metotunAsync olmasına gerek yok. Olmasında bir sakınca yok oluşabilecek farklı bağımlılıklar için Async yapıldı.
            var tcmbExchangeRateProvider = new TcmbExchangeRateProvider();
            tcmbExchangeRateProvider.Initialize(true);
            var response = new TcmbSearchResponse();
            var filteredItem = new List<TcmbExchangeRate>();
            var alldata = tcmbExchangeRateProvider.GetExchangeRates();
            if (request.Currencies!=null)
                filteredItem.AddRange(alldata.Where(c=>request.Currencies.Contains(c.Key)).Select(c=>c.Value));
            else
                filteredItem.AddRange(alldata.Values);

            IEnumerable<TcmbExchangeRate> items = filteredItem;
            var orderbys = request.OrderBy.GetFlags().Cast<OrderBy>();
            if (!orderbys.Any())
                orderbys = new List<OrderBy> { OrderBy.CurrencyAsc };
            var isAsc = request.OrderByType == OrderByType.Asc;
            IOrderedEnumerable<TcmbExchangeRate> orderedQuery = null;
            foreach (var orderby in orderbys)
            {

                switch (orderby)
                {
                    case OrderBy.CurrencyAsc:
                        if (orderedQuery != null)
                            orderedQuery = orderedQuery.ThenBy(c => c.Currency);
                        else
                            orderedQuery = items.OrderBy(c => c.Currency);

                        break;
                    case OrderBy.CurrencyDesc:
                        if (orderedQuery != null)
                            orderedQuery = orderedQuery.ThenByDescending(c => c.Currency);
                        else
                            orderedQuery = items.OrderByDescending(c => c.Currency);

                        break;
                    case OrderBy.ForexBuyingAsc:
                        if (orderedQuery != null)
                            orderedQuery = orderedQuery.ThenBy(c => c.ForexBuying);
                        else
                            orderedQuery = items.OrderBy(c => c.ForexBuying);
                        break;
                    case OrderBy.ForexBuyingDesc:
                        if (orderedQuery != null)
                            orderedQuery = orderedQuery.ThenByDescending(c => c.ForexBuying);
                        else
                            orderedQuery = items.OrderByDescending(c => c.ForexBuying);
                        break;
                    case OrderBy.ForexSellingAsc:
                        if (orderedQuery != null)
                            orderedQuery = orderedQuery.ThenBy(c => c.ForexSelling);
                        else
                            orderedQuery = items.OrderBy(c => c.ForexSelling);
                        break;
                    case OrderBy.ForexSellingDesc:
                        if (orderedQuery != null)
                            orderedQuery = orderedQuery.ThenByDescending(c => c.ForexSelling);
                        else
                            orderedQuery = items.OrderByDescending(c => c.ForexSelling);
                        break;
                    case OrderBy.BanknoteBuyingAsc:
                        if (orderedQuery != null)
                            orderedQuery = orderedQuery.ThenBy(c => c.BanknoteBuying);
                        else
                            orderedQuery = items.OrderBy(c => c.BanknoteBuying);
                        break;
                    case OrderBy.BanknoteBuyingDesc:
                        if (orderedQuery != null)
                            orderedQuery = orderedQuery.ThenByDescending(c => c.BanknoteBuying);
                        else
                            orderedQuery = items.OrderByDescending(c => c.BanknoteBuying);
                        break;
                    case OrderBy.BanknoteSellingAsc:
                        if (orderedQuery != null)
                            orderedQuery = orderedQuery.ThenBy(c => c.BanknoteSelling);
                        else
                            orderedQuery = items.OrderBy(c => c.BanknoteSelling);
                        break;
                    case OrderBy.BanknoteSellingDesc:
                        if (orderedQuery != null)
                            orderedQuery = orderedQuery.ThenByDescending(c => c.BanknoteSelling);
                        else
                            orderedQuery = items.OrderByDescending(c => c.BanknoteSelling);
                        break;
                    default:
                        break;
                }
            }
            response.OrderBy = request.OrderBy;
            response.OrderByType = request.OrderByType;
            response.Items = orderedQuery.ToList();
            return Task.FromResult(response);
        }

    }
}
