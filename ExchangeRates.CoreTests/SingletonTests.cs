using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ExchangeRates.Core.Export;
using ExchangeRates.Core;
using Assert = NUnit.Framework.Assert;

namespace ExchangeRates.Tests
{
    [TestClass()]
    [TestFixture()]
    public class SingletonTests
    {
        [Test()]
        [TestCase(20)]
        public async Task Multi_Thread_CsvExportManager_Instance(int threads)
        {
            var instances = new ConcurrentDictionary<int, CsvExportManager>();
            Parallel.For(0, threads, index =>
            {
                new Thread(() =>
                {
                    var i = Singleton<CsvExportManager>.Instance;
                    instances[i.GetHashCode()] = i;

                }).Start();
            });
            Assert.That(instances.Count == 1);
           
        }
    }
}