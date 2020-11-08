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
                        Console.WriteLine(currencyOrderList[i].Symbol);
                        Console.WriteLine(currencyOrderList[i].AskPrice);
                        Console.WriteLine(currencyOrderList[i].BidPrice);
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
