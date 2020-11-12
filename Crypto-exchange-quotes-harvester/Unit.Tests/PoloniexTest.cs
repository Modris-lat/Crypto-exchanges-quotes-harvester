using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poloniex.API.Client;

namespace Unit.Tests
{
    [TestClass]
    public class PoloniexTest
    {
        private IPoloniexClient poloniexClient;

        public PoloniexTest()
        {
            poloniexClient = new PoloniexClient();
        }

        [TestMethod]
        public async Task GetOrderBook()
        {
            var result = await poloniexClient.GetOrderBookTicker();
            Assert.IsTrue(result.Any());
        }
        [TestMethod]
        public async Task GetOrderBookWithEmptyList()
        {
            var result = await poloniexClient.GetOrderBookTicker();
            Assert.IsFalse(result == null);
        }
        [TestMethod]
        public async Task GetOrderBookContainsUSDTXRP()
        {
            var result = await poloniexClient.GetOrderBookTicker();
            var instrument = result.SingleOrDefault(o =>
                o.Symbol == "USDTXRP");
            Assert.IsTrue(instrument.Symbol == "USDTXRP");
        }
        [TestMethod]
        public async Task GetOrderBookContainsXRPUSDT()
        {
            var result = await poloniexClient.GetOrderBookTicker();
            var instrument = result.SingleOrDefault(o =>
                o.Symbol == "XRPUSDT");
            Assert.IsTrue(instrument == null);
        }
        [TestMethod]
        public async Task GetOrderBookContainsXRPBTC()
        {
            var result = await poloniexClient.GetOrderBookTicker();
            var instrument = result.SingleOrDefault(o =>
                o.Symbol == "XRPBTC");
            Assert.IsTrue(instrument == null);
        }
        [TestMethod]
        public async Task GetOrderBookContainsBTCXRP()
        {
            var result = await poloniexClient.GetOrderBookTicker();
            var instrument = result.SingleOrDefault(o =>
                o.Symbol == "BTCXRP");
            Assert.IsTrue(instrument.Symbol == "BTCXRP");
        }
    }
}
