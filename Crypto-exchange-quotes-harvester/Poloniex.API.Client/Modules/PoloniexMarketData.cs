using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Poloniex.API.Client.Interfaces;

namespace Poloniex.API.Client.Modules
{
    public class PoloniexMarketData
    {
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "asks")]
        public decimal AskPrice { get; set; }
        [JsonProperty(PropertyName = "bids")]
        public decimal BidPrice { get; set; }
        [JsonProperty(PropertyName = "isFrozen")]
        public int IsFrozen { get; set; }
    }
}
