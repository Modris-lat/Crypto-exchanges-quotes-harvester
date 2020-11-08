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
        private void DepthHandler(DepthMessage messageData)
        {
            var depthData = messageData;
        }

        [TestMethod]
        public void TestDepthEndpoint()
        {
            binanceClient.ListenDepthEndpoint("ethbtc", DepthHandler);
            Thread.Sleep(50000);
        }
        private void KlineHandler(KlineMessage messageData)
        {
            var klineData = messageData;
        }
        [TestMethod]
        public void TestKlineEndpoint()
        {
            binanceClient.ListenKlineEndpoint("ethbtc", TimeInterval.Minutes_1, KlineHandler);
            Thread.Sleep(50000);
        }
        private void AggregateTradesHandler(AggregateTradeMessage messageData)
        {
            var aggregateTrades = messageData;
        }

        [TestMethod]
        public void AggregateTestTradesEndpoint()
        {
            binanceClient.ListenTradeEndpoint("ethbtc", AggregateTradesHandler);
            Thread.Sleep(50000);
        }
        private void AccountHandler(AccountUpdatedMessage messageData)
        {
            var accountData = messageData;
        }

        private void TradesHandler(OrderOrTradeUpdatedMessage messageData)
        {
            var tradesData = messageData;
        }

        private void OrdersHandler(OrderOrTradeUpdatedMessage messageData)
        {
            var ordersData = messageData;
        }

        [TestMethod]
        public void TestUserDataEndpoint()
        {
            binanceClient.ListenUserDataEndpoint(AccountHandler, TradesHandler, OrdersHandler);
            Thread.Sleep(50000);
        }
    }
}
