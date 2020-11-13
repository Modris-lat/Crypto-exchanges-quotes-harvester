using Newtonsoft.Json;

namespace Binance.API.Client.Modules
{
    public class UserStreamInfo
    {
        [JsonProperty("listenKey")]
        public string ListenKey { get; set; }
    }
}
