using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Binance.API.Client;
using Core;
using Core.Interfaces;
using Core.Modules.Enums;
using Core.Modules.WebSockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit.Tests.Binance
{
    [TestClass]
    public class BinanceTest
    {
        private IAccessKeys keys;
        private IApiClient apiClient;
        private IBinanceClient binanceClient;
        public BinanceTest()
        {
            keys = new AccessKeys();
            apiClient = new ApiClient(keys.ApiKey, keys.ApiSecretKey);
            binanceClient = new BinanceClient(apiClient);
        }
        [TestMethod]
        public async Task GetServerTime()
        {
            var serverTime = await binanceClient.TestConnectivity();
        }

        [TestMethod]
        public async Task GetOrderBookTicker()
        {
            var orderBookTickers = await binanceClient.GetOrderBookTicker();
            Assert.IsTrue(orderBookTickers.Any());
        }

        [TestMethod]
        public async Task StartUserStream()
        {
            var userStreamInfo = await binanceClient.StartUserStream();
            var listenKey = userStreamInfo.ListenKey;
            Assert.IsTrue(!string.IsNullOrEmpty(listenKey));
        }

        [TestMethod]
        public async Task KeepAliveUserStream()
        {
            var userStreamInfo = await binanceClient.StartUserStream();
            var listenKey = userStreamInfo.ListenKey;
            var ping = await binanceClient.KeepAliveUserStream(listenKey);
        }

        [TestMethod]
        public async Task CloseUserStream()
        {
            var userStreamInfo = await binanceClient.StartUserStream();
            var listenKey = userStreamInfo.ListenKey;
            var result = await binanceClient.CloseUserStream(listenKey);
        }
        [TestMethod]
        public async Task GetOrderBookTickerBTCUSDT()
        {
            var result = await binanceClient.GetOrderBookTicker();
            var instrument = result.SingleOrDefault(o =>
                o.Symbol == "BTCUSDT");
            Assert.IsTrue(instrument.Symbol == "BTCUSDT");
        }
        [TestMethod]
        public async Task GetOrderBookTickerUSDTBTC()
        {
            var result = await binanceClient.GetOrderBookTicker();
            var instrument = result.SingleOrDefault(o =>
                o.Symbol == "USDTBTC");
            Assert.IsTrue(instrument == null);
        }
        [TestMethod]
        public async Task GetOrderBookTickerETHUSDT()
        {
            var result = await binanceClient.GetOrderBookTicker();
            var instrument = result.SingleOrDefault(o =>
                o.Symbol == "ETHUSDT");
            Assert.IsTrue(instrument.Symbol == "ETHUSDT");
        }
    }
}
