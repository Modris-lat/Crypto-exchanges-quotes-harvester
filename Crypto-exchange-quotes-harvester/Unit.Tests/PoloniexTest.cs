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

        PoloniexTest()
        {
            poloniexClient = new PoloniexClient();
        }
        [TestMethod]
        public async Task GetOrderBook()
        {
            var result = await poloniexClient.GetOrderBookTicker();
            Assert.IsTrue(result.Any());
        }
    }
}
