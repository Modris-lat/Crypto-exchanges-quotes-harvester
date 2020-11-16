namespace Core.Utils
{
    public class EndPoints
    {
        public static readonly string TestConnectivity = "/api/v1/ping";
        public static readonly string OrderBookTicker = "/api/v1/ticker/allBookTickers";
        public static readonly string StartUserStream = "/api/v1/userDataStream";
        public static readonly string KeepAliveUserStream = "/api/v1/userDataStream";
        public static readonly string CloseUserStream = "/api/v1/userDataStream";
    }
}
