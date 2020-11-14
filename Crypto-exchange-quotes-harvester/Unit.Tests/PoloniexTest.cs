using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance.API.Client.Interfaces;
using Binance.API.Client.Service;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poloniex.API.Client;
using Poloniex.API.Client.Interfaces;
using Poloniex.API.Client.Service;

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
        [TestMethod]
        public async Task SearchMethodBTCXRP()
        {
            ICalculateSyntheticQuotes calculator = new CalculateSyntheticQuotes();
            IPoloniexSearch searchMethod = new PoloniexSearch(calculator);
            var searchList = new List<SearchInstrument>
            {
                new SearchInstrument
                {
                    Symbol = "BTCXRP"
                }
            };
            var result = await poloniexClient.GetOrderBookTicker();
            var quotesList = await searchMethod.Search(result, searchList);
            Assert.IsTrue(quotesList[0].Name == "BTCXRP" && quotesList[0].Exchange == "Poloniex");
        }
        [TestMethod]
        public async Task SearchMethodFalse()
        {
            ICalculateSyntheticQuotes calculator = new CalculateSyntheticQuotes();
            IPoloniexSearch searchMethod = new PoloniexSearch(calculator);
            var searchList = new List<SearchInstrument>
            {
                new SearchInstrument
                {
                    Symbol = "XXXXXX"
                }
            };
            var result = await poloniexClient.GetOrderBookTicker();
            var quotesList = await searchMethod.Search(result, searchList);
            Assert.IsFalse(quotesList.Any());
        }
        [TestMethod]
        public async Task SearchMethodBTCUSDTSynthetics()
        {
            ICalculateSyntheticQuotes calculator = new CalculateSyntheticQuotes();
            IPoloniexSearch searchMethod = new PoloniexSearch(calculator);
            var searchList = new List<SearchInstrument>
            {
                new SearchInstrument
                {
                    Symbol = "BTCUSDT",
                    Synthetic1 = new Synthetic
                    {
                        SearchName = "USDTXRP",
                    },
                    Synthetic2 = new Synthetic
                    {
                        SearchName = "BTCXRP"
                    }
                }
            };
            var result = await poloniexClient.GetOrderBookTicker();
            var quotesList = await searchMethod.Search(result, searchList);
            Assert.IsTrue(quotesList[0].Name == "BTCUSDT" && quotesList[0].Exchange == "Poloniex");
        }
    }
}
