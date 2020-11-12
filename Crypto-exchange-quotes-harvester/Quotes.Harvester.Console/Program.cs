using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.API.Client;
using Core;
using Core.Interfaces;
using Core.Models;
using Poloniex.API.Client;

namespace Quotes.Harvester.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IAccessKeys keys = new AccessKeys();
            IApiClient api = new ApiClient(keys.ApiKey, keys.ApiSecretKey);
            IBinanceClient binanceClient = new BinanceClient(api);
            IPoloniexClient poloniexClient = new PoloniexClient();
            ISettingsConfig settings = new DataBaseSettings();
            settings.ChooseSettings();
            var collectedQuotes = new List<Quote>();
            var streamInfo = await binanceClient.StartUserStream();
            var listenKey = streamInfo.ListenKey;
            bool loop = true;
            while (loop)
            {
                var binanceMarketInfo = await binanceClient.GetOrderBookTicker();
                var currencyOrderList = binanceMarketInfo.ToList();
                for (int i = 0; i < currencyOrderList.Count; i++)
                {
                    for (int j = 0; j < settings.Instruments.Count; j++)
                    {
                        if (currencyOrderList[i].Symbol == settings.Instruments[j].Symbol)
                        {
                            var quote = new Quote
                            {
                                Id = collectedQuotes.Count,
                                Name = settings.Instruments[j].Symbol,
                                Time = DateTime.Now.ToLongDateString(),
                                Bid = currencyOrderList[i].BidPrice,
                                Ask = currencyOrderList[i].AskPrice,
                                Exchange = "Binance"
                            };
                            collectedQuotes.Add(quote);
                        }
                    }
                }
                var poloniexMarketInfo = await poloniexClient.GetOrderBookTicker();
                var poloniexList = poloniexMarketInfo.ToList();
                for (int i = 0; i < poloniexList.Count; i++)
                {
                    for (int j = 0; j < settings.Instruments.Count; j++)
                    {
                        if (poloniexList[i].Symbol == settings.Instruments[j].Symbol)
                        {
                            var quote = new Quote
                            {
                                Id = collectedQuotes.Count,
                                Name = settings.Instruments[j].Symbol,
                                Time = DateTime.Now.ToLongDateString(),
                                Bid = poloniexList[i].BidPrice,
                                Ask = poloniexList[i].AskPrice,
                                Exchange = "Poloniex"
                            };
                            collectedQuotes.Add(quote);
                        }
                    }
                }
                System.Console.WriteLine(collectedQuotes.Count);
                var input = System.Console.ReadKey().KeyChar;
                if (input == 'q')
                {
                    loop = false;
                }
            }

            await binanceClient.CloseUserStream(listenKey);
        }
    }
}
