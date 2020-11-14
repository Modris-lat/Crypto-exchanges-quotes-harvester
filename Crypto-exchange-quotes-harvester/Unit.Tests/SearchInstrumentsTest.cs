using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models;
using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit.Tests
{
    [TestClass]
    public class SearchInstrumentsTest
    {
        private IGetSearchInstruments searchList;

        public SearchInstrumentsTest()
        {
            searchList = new GetSearchInstruments();
        }
        [TestMethod]
        public void TestMethod1()
        {
            var settingsList = new List<Instrument> { };
            var result = searchList.SearchInstrumentList(settingsList);
            Assert.IsFalse(result.Any());
        }
        [TestMethod]
        public void TestMethod2()
        {
            var settingsList = new List<Instrument>
            {
                new Instrument {Symbol = "BTC/USDT"},
                new Instrument {Symbol = "ETH/USDT"},
                new Instrument {Symbol = "BTC/USDT", Depends = new Depend{Synthetic1 = "ETH/BTC", Synthetic2 = "ETH/USDT"}},
                new Instrument {Symbol = "BTC/USDT", Depends = new Depend{Synthetic1 = "XRP/BTC", Synthetic2 = "XRP/USDT"}}
            };
            var result = searchList.SearchInstrumentList(settingsList);
            Assert.IsTrue(result.Any());
        }
        [TestMethod]
        public void TestMethod3()
        {
            var settingsList = new List<Instrument>
            {
                new Instrument {Symbol = "BTC/USDT"},
                new Instrument {Symbol = "BTC/USDT", Depends = new Depend{Synthetic1 = "ETH/BTC", Synthetic2 = "ETH/USDT"}}
            };
            var result = searchList.SearchInstrumentList(settingsList);
            Assert.IsTrue(
                result[0].Symbol == "BTCUSDT" && result[0].Synthetic1 == null && result[0].Synthetic2 == null &&
                result[1].Symbol == "BTC/USDT" && result[1].Synthetic1.Symbol == "ETH/BTC" &&
                result[1].Synthetic1.SearchName == "ETHBTC" && result[1].Synthetic2.SearchName == "ETHUSDT" &&
                result[1].Synthetic2.Symbol == "ETH/USDT");
        }
    }
}
