using Newtonsoft.Json;

namespace Core.Modules
{
    public class ServerInfo
    {
        [JsonProperty("serverTIme")]
        public long ServerTime { get; set; }
    }
}
