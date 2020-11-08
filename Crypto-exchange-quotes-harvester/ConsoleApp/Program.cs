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
            IBinanceMarketInfo getBinanceMarketInfo = new BinanceMarketInfo(new BinanceClient(api));
            var streamInfo = await getBinanceMarketInfo.BinanceClient.StartUserStream();
            var listenKey = streamInfo.ListenKey;
            bool loop = true;
            while (loop)
            {
                await getBinanceMarketInfo.GetBinanceMarketInfo();
                var input = Console.ReadKey().KeyChar;
                if (input == 'a')
                {
                    loop = false;
                }
            }

            await getBinanceMarketInfo.BinanceClient.CloseUserStream(listenKey);
        }
    }
}
