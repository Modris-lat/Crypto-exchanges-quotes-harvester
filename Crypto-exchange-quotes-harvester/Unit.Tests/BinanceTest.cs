using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance.API.Client;
using Binance.API.Client.Interfaces;
using Binance.API.Client.Service;
using Core;
using Core.Interfaces;
using Core.Models;
using Core.Services;
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
        [TestMethod]
        public async Task GetOrderBookTickerETHBTC()
        {
            var result = await binanceClient.GetOrderBookTicker();
            var instrument = result.SingleOrDefault(o =>
                o.Symbol == "ETHBTC");
            Assert.IsTrue(instrument.Symbol == "ETHBTC");
        }
        [TestMethod]
        public async Task GetOrderBookTickerXRPBTC()
        {
            var result = await binanceClient.GetOrderBookTicker();
            var instrument = result.SingleOrDefault(o =>
                o.Symbol == "XRPBTC");
            Assert.IsTrue(instrument.Symbol == "XRPBTC");
        }
        [TestMethod]
        public async Task GetOrderBookTickerXRPUSDT()
        {
            var result = await binanceClient.GetOrderBookTicker();
            var instrument = result.SingleOrDefault(o =>
                o.Symbol == "XRPUSDT");
            Assert.IsTrue(instrument.Symbol == "XRPUSDT");
        }
        [TestMethod]
        public async Task SearchMethodBTCUSDT()
        {
            ICalculateSyntheticQuotes calculator = new CalculateSyntheticQuotes();
            IBinanceSearch searchMethod = new BinanceSearch(calculator);
            var searchList = new List<SearchInstrument>
            {
                new SearchInstrument
                {
                    Symbol = "BTCUSDT"
                },
            };
            var result = await binanceClient.GetOrderBookTicker();
            var quotesList = await searchMethod.Search(result, searchList);
            Assert.IsTrue(quotesList[0].Name == "BTCUSDT");
        }
        [TestMethod]
        public async Task SearchMethodBTCUSDTFalse()
        {
            ICalculateSyntheticQuotes calculator = new CalculateSyntheticQuotes();
            IBinanceSearch searchMethod = new BinanceSearch(calculator);
            var searchList = new List<SearchInstrument>
            {
                new SearchInstrument
                {
                    Symbol = "BTC/USDT"
                },
            };
            var result = await binanceClient.GetOrderBookTicker();
            var quotesList = await searchMethod.Search(result, searchList);
            Assert.IsFalse(quotesList.Any());
        }
        [TestMethod]
        public async Task SearchMethodBTCUSDTSynthetics()
        {
            ICalculateSyntheticQuotes calculator = new CalculateSyntheticQuotes();
            IBinanceSearch searchMethod = new BinanceSearch(calculator);
            var searchList = new List<SearchInstrument>
            {
                new SearchInstrument
                {
                    Symbol = "BTCUSDT",
                    Synthetic1 = new Synthetic
                    {
                        SearchName = "ETHBTC",
                        Symbol = "ETH/BTC"
                    },
                    Synthetic2 = new Synthetic
                    {
                        SearchName = "ETHUSDT",
                        Symbol = "ETH/USDT"
                    }
                }
            };
            var result = await binanceClient.GetOrderBookTicker();
            var quotesList = await searchMethod.Search(result, searchList);
            Assert.IsTrue(quotesList[0].Name == "BTCUSDT" && quotesList[0].Exchange == "Binance");
        }
    }
}
