using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poloniex.API.Client;
using Poloniex.API.Client.API;
using Poloniex.API.Client.Interfaces;

namespace Unit.Tests
{
    [TestClass]
    public class PoloniexTest
    {
        private IPoloniexClient poloniexClient;

        PoloniexTest()
        {
            poloniexClient = new PoloniexClient();
        }
        [TestMethod]
        public async Task GetOrderBook()
        {
            //var result = await poloniexClient.GetOrderBookTicker();
        }
    }
}
