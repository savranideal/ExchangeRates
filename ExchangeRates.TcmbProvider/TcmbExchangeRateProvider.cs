using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ExchangeRates;
using ExchangeRates.Core;
using Newtonsoft.Json;

namespace ExchangeRates.TcmbProvider
{
    public class TcmbExchangeRateProvider : IExchangeProvider<TcmbExchangeRate>
    {
        private const string _host = "https://www.tcmb.gov.tr/kurlar/today.xml";
        private static readonly ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();
        private static readonly CancellationTokenSource CancelToken = new CancellationTokenSource();
        private static readonly TimeSpan _sleep = new TimeSpan(1, 0, 0);
        private static readonly Thread _backgroundThread = new Thread(Worker)
        {
            IsBackground = true,
            Name = nameof(TcmbExchangeRateProvider)
        };


        /// <summary>
        ///  Uygulama bağımlılığımız yok. ilk andan start edebiliriz.
        /// </summary>
        static TcmbExchangeRateProvider()
        {
            _backgroundThread.Start();
        }


        /// <summary>
        /// <see cref="scope"/> arkaplanda çalışacak olan işlem için çalışma alanıbı belirmektedir.
        /// </summary>
        /// <param name="scope"></param>
        private static void Worker()
        {

            //asyn metodları kullanabilmek için oluşturduk.
            Task.Run(async () =>
            {
                while (!CancelToken.IsCancellationRequested)
                {
                    try
                    {

                        using (var serviceScope = ServiceFlowScope.Begin())
                        {
                            try
                            {
                                Tarih_Date remoteResponse = null;
                                using (var client = new RestClient())
                                {

                                    client.Timeout = TimeSpan.FromSeconds(60);
                                    using (var reqMsg = new HttpRequestMessage(HttpMethod.Post, _host))
                                    {
                                        using (var response = await client.SendAsync(reqMsg))
                                        {
                                            if (response.Content.Headers.ContentEncoding.Contains("gzip"))
                                            {
                                                var content = await response.Content.ReadAsByteArrayAsync();
                                                if (response.IsSuccessStatusCode)
                                                {
                                                    if (content != null)
                                                    {
                                                        using (var inputStream = new MemoryStream(content))
                                                        using (var gzip = new GZipStream(inputStream, CompressionMode.Decompress))
                                                        using (var reader = new StreamReader(gzip))
                                                        {
                                                            var q = await reader.ReadToEndAsync();
                                                            remoteResponse = Xml.Deserialize<Tarih_Date>(q);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                var content = await response.Content.ReadAsStringAsync();
                                                if (response.IsSuccessStatusCode)
                                                {
                                                    if (content != "[]" && content != null)
                                                    {
                                                        remoteResponse = Xml.Deserialize<Tarih_Date>(content);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                                if (remoteResponse != null)
                                {
                                    if (remoteResponse.Currency != null)
                                    {
                                        /// var olan veriyi merge etmiyoruz eziyoruz.
                                        IDictionary<Currency, TcmbExchangeRate> convertedData = new Dictionary<Currency, TcmbExchangeRate>();
                                        foreach (var item in remoteResponse.Currency)
                                        {
                                            if (TryParseCurrency(item.CurrencyCode, out Currency? currency))
                                            {
                                                convertedData[currency.Value] = new TcmbExchangeRate
                                                {

                                                    Value = item.ForexSelling.ToDecimal(),
                                                    ForexSelling = item.ForexSelling.ToDecimal(),
                                                    BanknoteBuying = item.BanknoteBuying.ToDecimal(),
                                                    BanknoteSelling = item.BanknoteSelling.ToDecimal(),
                                                    Currency = currency.Value,
                                                    ForexBuying = item.ForexBuying.ToDecimal(),
                                                    Order = item.CrossOrder,
                                                    CrossRateUSD = item.CrossRateUSD.ToDecimal()
                                                };
                                            }

                                        }
                                        _rates = new ReadOnlyDictionary<Currency, TcmbExchangeRate>(convertedData);
                                    }

                                }

                                Interlocked.CompareExchange(ref _initialized, 1, 0);
                            }
                            catch (Exception e)
                            {
                                //todo: logla
                            }
                            finally
                            { 
                                //// Log(serviceScope.Request)
                                //// Log(serviceScope.Response) 
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        //todo: logla
                    }

                    await Task.Delay(_sleep, CancelToken.Token);
                }
            }, CancelToken.Token)
                .Wait(CancelToken.Token);



        }


        private static int _initialized;

        public static bool Initialized => _initialized > 0;

        /// <summary>
        /// Çalışır hale gelmesi bekler ve mevcut thread'i timeout süresi kadar kilitler.
        /// </summary>
        public static void WaitInitialization(bool waitInfinite = false)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var initializeTask = Task.Run(async () =>
            {
                while (!Initialized && !cancellationTokenSource.IsCancellationRequested)
                {
                    await Task.Delay(50, cancellationTokenSource.Token);
                }
            }, cancellationTokenSource.Token);
            if (!initializeTask.Wait(15500, cancellationTokenSource.Token))
            {
                cancellationTokenSource.Cancel();
                throw new TimeoutException($"The {nameof(TcmbExchangeRateProvider)} haven't initialized yet.");
            }
        }

        public void Initialize(bool waitForFinish = false) => WaitInitialization(waitForFinish);


        private const string _nullCodeStr = "NULL";
        private static bool TryParseCurrency(string currencyString, out Currency? currency)
        {
            if (currencyString == _nullCodeStr || currencyString == string.Empty) //eğer gelen string NULL ise zero için başarlı kabul edelim.
            {
                currency = Currency.NULL;
                return true;
            }

            if (!Constants.CurrencyFromIsoCode.ContainsKey(currencyString))
            {
                currency = Currency.NULL;
                return false;
            }

            currency = Constants.CurrencyFromIsoCode[currencyString];
            return true;
        }
        /// <summary>
        ///  Kur dönüşümü yapar
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public TcmbExchangeRate GetExchangeRate(Currency currency)
        {
            WaitInitialization(true);
            if (_rates.ContainsKey(currency))
                return _rates[currency];
            throw new KeyNotFoundException($"{currency} not found");
        }
         

        public IDictionary<Currency, TcmbExchangeRate> GetExchangeRates()
        {
            WaitInitialization(true);
            return _rates;
        }

        private static ReadOnlyDictionary<Currency, TcmbExchangeRate> _rates;

        public static ReadOnlyDictionary<Currency, TcmbExchangeRate> Rates
        {
            get
            {
                using (_locker.ReadLock())
                {
                    return _rates;
                }
            }
            set
            {
                using (_locker.WriteLock())
                {
                    _rates = value;
                }
            }
        }

    }
}
