using System.Threading.Tasks;

namespace ExchangeRates
{
    public interface ISearchable<TRequest, TResponse,TModel>
        where TRequest : class
        where TResponse : SearchResponse<TModel>
        where TModel :ExchangeRate
    {
        Task<TResponse> SearchAsync(TRequest request);

    }


}
