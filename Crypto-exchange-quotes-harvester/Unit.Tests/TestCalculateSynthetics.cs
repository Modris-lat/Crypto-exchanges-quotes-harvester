using System;
using System.Collections.Generic;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit.Tests
{
    [TestClass]
    public class TestCalculateSynthetics
    {
        private ICalculateSyntheticQuotes calculator;
        private List<Synthetic> synthetics;
        private DataBaseSettings settings;

        public TestCalculateSynthetics()
        {
            calculator = new CalculateSyntheticQuotes();
            synthetics = new List<Synthetic>
            {
                new Synthetic
                {
                    Exchange = "Binance",
                    Bid = 7698,
                    Ask = 7700,
                    Symbol = "BTC/EUR"
                },
                new Synthetic
                {
                    Exchange = "Binance",
                    Bid = 0.84M,
                    Ask = 0.90M,
                    Symbol = "USDT/EUR"
                }
            };
            settings = new DataBaseSettings
            {
                Instruments = new List<Instrument>
                {
                    new Instrument
                    {
                        Symbol = "BTC/USDT",
                        Depends = new Depend
                        {
                            Synthetic1 = "BTC/EUR",
                            Synthetic2 = "USDT/EUR"
                        }
                    }
                }
            };
        }
        [TestMethod]
        public void TestMethod1()
        {
            var quote = calculator.Calculate(synthetics, settings);
            Assert.IsTrue(quote.Exchange == "Binance" && quote.Name == "BTC/USDT" && quote.Bid == 9165 &&
                          quote.Ask == 8556);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var quote = calculator.Calculate(synthetics, settings);
            Assert.IsFalse(quote.Exchange == "Binance" && quote.Name == "BTC/USDT" && quote.Bid == 9155 &&
                          quote.Ask == 1556);
        }
    }
}
