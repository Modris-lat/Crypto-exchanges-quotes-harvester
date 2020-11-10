using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Modules.WebSockets
{
    public class AccountUpdatedMessage
    {
        [JsonProperty("e")]
        public string EventType { get; set; }
        [JsonProperty("E")]
        public long EventTime { get; set; }
        [JsonProperty("m")]
        public int MakerCommission { get; set; }
        [JsonProperty("t")]
        public int TakerCommission { get; set; }
        [JsonProperty("b")]
        public int BuyerCommission { get; set; }
        [JsonProperty("s")]
        public int SellerCommission { get; set; }
        [JsonProperty("t")]
        public bool CanTrade { get; set; }
        [JsonProperty("w")]
        public bool CanWithdraw { get; set; }
        [JsonProperty("d")]
        public bool CanDeposit { get; set; }
        [JsonProperty("B")]
        public IEnumerable<Balance> Balances { get; set; }
    }
    public class Balance
    {
        [JsonProperty("a")]
        public string Asset { get; set; }
        [JsonProperty("f")]
        public decimal Free { get; set; }
        [JsonProperty("l")]
        public decimal Locked { get; set; }
    }
}
