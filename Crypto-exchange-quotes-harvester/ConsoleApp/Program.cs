using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.API.Client;
using Core;
using Core.Interfaces;
using Core.Modules.Enums;
using Core.Modules.WebSockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Poloniex.API.Client;
using Poloniex.API.Client.API;
using Poloniex.API.Client.Interfaces;
using Poloniex.API.Client.Modules;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IAccessKeys keys = new AccessKeys();
            IApiClient api = new ApiClient(keys.ApiKey, keys.ApiSecretKey);
            IBinanceClient binanceClient = new BinanceClient(api);
            IPoloniexClient poloniexClient = new PoloniexClient();
            var streamInfo = await binanceClient.StartUserStream();
            var listenKey = streamInfo.ListenKey;
            bool loop = true;
            while (loop)
            {
                var binanceMarketInfo = await binanceClient.GetOrderBookTicker();
                var currencyOrderList = binanceMarketInfo.ToList();
                for (int i = 0; i < currencyOrderList.Count; i++)
                {
                    if (currencyOrderList[i].Symbol == "ETHBTC")
                    {
                        Console.WriteLine("Binance market:");
                        Console.WriteLine(currencyOrderList[i].Symbol);
                        Console.WriteLine(currencyOrderList[i].AskPrice);
                        Console.WriteLine(currencyOrderList[i].BidPrice);
                    }
                }
                var poloniexMarketInfo = await poloniexClient.GetOrderBookTicker();
                var poloniexList = poloniexMarketInfo.ToList();
                for (int i = 0; i < poloniexList.Count; i++)
                {
                    if (i == 1)
                    {
                        Console.WriteLine("Poloniex market:");
                        Console.WriteLine($"{poloniexList[i].Symbol}");
                        Console.WriteLine($"{poloniexList[i].AskPrice}");
                        Console.WriteLine($"{poloniexList[i].BidPrice}");
                        Console.WriteLine($"{poloniexList[i].IsFrozen}");
                    }
                }
                var input = Console.ReadKey().KeyChar;
                if (input == 'q')
                {
                    loop = false;
                }
            }

            await binanceClient.CloseUserStream(listenKey);
        }
    }
}
