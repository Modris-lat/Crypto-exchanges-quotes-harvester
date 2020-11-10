using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poloniex.API.Client.Modules
{
    public class CurrencyPair
    {
        public string Pair { get; set; }
        public PoloniexMarketData MarketData { get; set; }
    }
}
