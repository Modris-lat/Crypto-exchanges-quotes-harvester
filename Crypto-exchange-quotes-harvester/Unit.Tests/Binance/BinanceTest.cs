using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using Binance.API.Client;
using Core;
using Core.Interfaces;
using Core.Modules.Enums;
using Core.Modules.WebSockets;

namespace Binance.API.Csharp.Client.Test
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
        public void GetOrderBookTicker()
        {
            var orderBookTickers = binanceClient.GetOrderBookTicker().Result;
        }

        [TestMethod]
        public void StartUserStream()
        {
            var listenKey = binanceClient.StartUserStream().Result.ListenKey;
        }

        [TestMethod]
        public void KeepAliveUserStream()
        {
            var listenKey = binanceClient.StartUserStream().Result.ListenKey;
            var ping = binanceClient.KeepAliveUserStream(listenKey).Result;
        }

        [TestMethod]
        public void CloseUserStream()
        {
            var listenKey = binanceClient.StartUserStream().Result.ListenKey;
            var ping = binanceClient.KeepAliveUserStream(listenKey).Result;
            var resut = binanceClient.CloseUserStream(listenKey).Result;
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
