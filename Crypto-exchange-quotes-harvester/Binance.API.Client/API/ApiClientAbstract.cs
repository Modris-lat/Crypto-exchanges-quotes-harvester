using System;
using System.Collections.Generic;
using System.Net.Http;
using Harvested.Quotes.Data.Interfaces;
using WebSocketSharp;

namespace Core
{
    public abstract class ApiClientAbstract
    {
        public readonly string _apiUrl = "";
        public readonly string _apiKey = "";
        public readonly string _apiSecret = "";
        public readonly HttpClient _httpClient;
        public readonly string _webSocketEndpoint = "";
        public List<WebSocket> _openSockets;
        public delegate void MessageHandler<T>(T messageData);
        public ApiClientAbstract(string apiKey, string apiSecret, string apiUrl = @"https://www.binance.com", string webSocketEndpoint = @"wss://stream.binance.com:9443/ws/", bool addDefaultHeaders = true)
        {
            _apiUrl = apiUrl;
            _apiKey = apiKey;
            _apiSecret = apiSecret;
            _webSocketEndpoint = webSocketEndpoint;
            _openSockets = new List<WebSocket>();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_apiUrl)
            };

            if (addDefaultHeaders)
            {
                ConfigureHttpClient();
            }
        }
        private void ConfigureHttpClient()
        {
            _httpClient.DefaultRequestHeaders
                .Add("X-MBX-APIKEY", _apiKey);

            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
