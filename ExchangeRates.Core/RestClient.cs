using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeRates
{
    /// <summary>
    /// Loglama yapılabilmesi için HttpClient yapısına özel bir handler ekler.
    /// Loglama için HttpClient yerine bunu kullanılabilir.
    /// </summary>
    public class RestClient : HttpClient
    {
        /// <summary>
        /// Yeni bir RestClient oluşturur.
        /// </summary>
        public RestClient(bool logRawRequest = true, bool logRawResponse = true, bool allowAutoRedirect = true)
            : this(new HttpClientHandler { AllowAutoRedirect = allowAutoRedirect }, logRawRequest, logRawResponse)
        {
        }

        public RestClient(HttpMessageHandler handler, bool logRawRequest = true, bool logRawResponse = true)
            : base(new LoggingHandler(handler, logRawRequest, logRawResponse))
        {
        }

        /// <summary>
        /// Rest işlemlerini loglamak için kullanılır.
        /// </summary>
        public class LoggingHandler : DelegatingHandler
        {
            private readonly bool _logRawRequest;
            private readonly bool _logRawResponse;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="innerHandler"></param>
            public LoggingHandler(HttpMessageHandler innerHandler, bool logRawRequest = true, bool logRawResponse = true)
                : base(innerHandler)
            {
                _logRawRequest = logRawRequest;
                _logRawResponse = logRawResponse;
            }

            /// <inheritdoc />
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var hasScope = !ServiceFlowScope.Current.IsNull;

                if (_logRawRequest && hasScope)
                {
                    ServiceFlowScope.Current.RequestHeaders = request.ToString();

                    //content null değilse post'tur.
                    if (request.Content != null)
                    {
                        ServiceFlowScope.Current.Request = await request.Content.ReadAsStringAsync();
                        if (request.Content is FormUrlEncodedContent)
                            ServiceFlowScope.Current.Request = Uri.UnescapeDataString(ServiceFlowScope.Current.Request)
                                .Replace("?", "?\r\n")
                                .Replace("&", "&\r\n");
                    }
                    else //get
                    {
                        ServiceFlowScope.Current.Request = request.RequestUri.ToString()
                            .Replace("?", "?\r\n")
                            .Replace("&", "&\r\n");
                    }
                }

                

                HttpResponseMessage response;
                try
                {
                    response = await base.SendAsync(request, cancellationToken);
                }
                finally
                {
                    
                }


                if (_logRawResponse && hasScope)
                {
                    ServiceFlowScope.Current.ResponseHeaders = response.ToString();
                    if (response.Content != null)
                    {
                        ServiceFlowScope.Current.Response = await response.Content.ReadAsStringAsync();
                    }
                }

                return response;
            }
        }
    }


}
