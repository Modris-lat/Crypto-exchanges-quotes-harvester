
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.API.Client.Modules;
using Core.Modules;
using Core.Modules.Enums;
using Core.Modules.Market;
using Core.Modules.WebSockets;

namespace Core.Interfaces
{
    public interface IBinanceClient
    {
        Task<dynamic> TestConnectivity();
        Task<IEnumerable<OrderBookTicker>> GetOrderBookTicker();
        Task<UserStreamInfo> StartUserStream();
        Task<dynamic> KeepAliveUserStream(string listenKey);
        Task<dynamic> CloseUserStream(string listenKey);
        void ListenDepthEndpoint(string symbol, ApiClientAbstract.MessageHandler<DepthMessage> messageHandler);
        void ListenKlineEndpoint(string symbol, TimeInterval interval, ApiClientAbstract.MessageHandler<KlineMessage> messageHandler);
        void ListenTradeEndpoint(string symbol, ApiClientAbstract.MessageHandler<AggregateTradeMessage> messageHandler);
        string ListenUserDataEndpoint(ApiClientAbstract.MessageHandler<AccountUpdatedMessage> accountInfoHandler, ApiClientAbstract.MessageHandler<OrderOrTradeUpdatedMessage> tradesHandler, ApiClientAbstract.MessageHandler<OrderOrTradeUpdatedMessage> ordersHandler);
    }
}
