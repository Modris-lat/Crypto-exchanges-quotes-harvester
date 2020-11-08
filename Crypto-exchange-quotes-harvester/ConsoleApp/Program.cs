using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Binance.API.Client;
using Core;
using Core.Interfaces;
using Core.Modules.Enums;
using Core.Modules.WebSockets;
using Newtonsoft.Json.Linq;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IAccessKeys keys = new AccessKeys();
            IApiClient api = new ApiClient(keys.ApiKey, keys.ApiSecretKey);
            IBinanceClient binanceClient = new BinanceClient(api);
            var userStreamInfo = await binanceClient.StartUserStream();
            var listenerKey = userStreamInfo.ListenKey;
            bool loop = true;
            while (loop)
            {
                var orderBookList = await binanceClient.GetOrderBookTicker();
                var marketInfo = orderBookList.ToList();
                for (int i = 0; i < marketInfo.Count(); i++)
                {
                    if (marketInfo[i].Symbol == "ETHBTC")
                    {
                        Console.WriteLine(marketInfo[i].AskPrice);
                        Console.WriteLine(marketInfo[i].BidPrice);
                        Console.WriteLine(marketInfo[i].Symbol);
                        var input = Console.ReadKey().KeyChar;
                        if (input == 'q')
                        {
                            break;
                        }

                        if (input == 'a')
                        {
                            loop = false;
                        }
                    }
                }
            }

            await binanceClient.CloseUserStream(listenerKey);
        }
    }
}
