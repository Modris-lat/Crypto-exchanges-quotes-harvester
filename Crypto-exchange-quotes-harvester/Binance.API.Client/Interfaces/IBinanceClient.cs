using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.API.Client.Modules;
using Core.Modules.Enums;
using Core.Modules.Market;
using Core.Modules.WebSockets;

namespace Core.Interfaces
{
    public interface IBinanceClient
    {
        Task<dynamic> TestConnectivity();
        Task<List<OrderBookTicker>> GetOrderBookTicker();
        Task<UserStreamInfo> StartUserStream();
        Task<dynamic> KeepAliveUserStream(string listenKey);
        Task<dynamic> CloseUserStream(string listenKey);
    }
}
