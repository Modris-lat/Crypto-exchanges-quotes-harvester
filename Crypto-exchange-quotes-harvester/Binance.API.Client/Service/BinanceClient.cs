using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public async Task<List<OrderBookTicker>> GetOrderBookTicker()
        {
            var result = await _apiClient.CallAsync<IEnumerable<OrderBookTicker>>(ApiMethod.GET, EndPoints.OrderBookTicker, false);

            return result.ToList();
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
    }
}
