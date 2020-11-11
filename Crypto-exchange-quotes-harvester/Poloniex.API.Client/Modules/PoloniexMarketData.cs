using Newtonsoft.Json;

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
