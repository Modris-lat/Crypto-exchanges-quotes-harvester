using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.API.Client.Modules;
using Binance.API.Client.Modules.Enums;
using Core;
using Core.Interfaces;
using Core.Modules.Enums;
using Core.Modules.Market;
using Core.Modules.WebSockets;
using Core.Utils;

namespace Binance.API.Client
{
    public class BinanceClient: BinanceClientAbstract, IBinanceClient
    {
        public BinanceClient(IApiClient apiClient) : base(apiClient) { }
        public async Task<dynamic> TestConnectivity()
        {
            var result = await _apiClient.CallAsync<dynamic>(ApiMethod.GET, EndPoints.TestConnectivity, false);

            return result;
        }
        
        public async Task<IEnumerable<OrderBookTicker>> GetOrderBookTicker()
        {
            var result = await _apiClient.CallAsync<IEnumerable<OrderBookTicker>>(ApiMethod.GET, EndPoints.OrderBookTicker, false);

            return result;
        }

        public async Task<UserStreamInfo> StartUserStream()
        {
            var result = await _apiClient.CallAsync<UserStreamInfo>(ApiMethod.POST, EndPoints.StartUserStream, false);

            return result;
        }

        public async Task<dynamic> KeepAliveUserStream(string listenKey)
        {
            if (string.IsNullOrWhiteSpace(listenKey))
            {
                throw new ArgumentException("listenKey cannot be empty. ", "listenKey");
            }

            var result = await _apiClient.CallAsync<dynamic>(ApiMethod.PUT, EndPoints.KeepAliveUserStream, false, $"listenKey={listenKey}");

            return result;
        }

        public async Task<dynamic> CloseUserStream(string listenKey)
        {
            if (string.IsNullOrWhiteSpace(listenKey))
            {
                throw new ArgumentException("listenKey cannot be empty. ", "listenKey");
            }

            var result = await _apiClient.CallAsync<dynamic>(ApiMethod.DELETE, EndPoints.CloseUserStream, false, $"listenKey={listenKey}");

            return result;
        }

        public void ListenDepthEndpoint(string symbol, ApiClientAbstract.MessageHandler<DepthMessage> depthHandler)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentException("symbol cannot be empty. ", "symbol");
            }

            var param = symbol + "@depth";
            _apiClient.ConnectToWebSocket(param, depthHandler, true);
        }

        public void ListenKlineEndpoint(string symbol, TimeInterval interval, ApiClientAbstract.MessageHandler<KlineMessage> klineHandler)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentException("symbol cannot be empty. ", "symbol");
            }

            var param = symbol + $"@kline_{interval.GetDescription()}";
            _apiClient.ConnectToWebSocket(param, klineHandler);
        }

        public void ListenTradeEndpoint(string symbol, ApiClientAbstract.MessageHandler<AggregateTradeMessage> tradeHandler)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentException("symbol cannot be empty. ", "symbol");
            }

            var param = symbol + "@aggTrade";
            _apiClient.ConnectToWebSocket(param, tradeHandler);
        }

        public string ListenUserDataEndpoint(ApiClientAbstract.MessageHandler<AccountUpdatedMessage> accountInfoHandler, ApiClientAbstract.MessageHandler<OrderOrTradeUpdatedMessage> tradesHandler, ApiClientAbstract.MessageHandler<OrderOrTradeUpdatedMessage> ordersHandler)
        {
            var listenKey = StartUserStream().Result.ListenKey;

            _apiClient.ConnectToUserDataWebSocket(listenKey, accountInfoHandler, tradesHandler, ordersHandler);

            return listenKey;
        }
    }
}
