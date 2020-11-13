using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.API.Client;
using Core;
using Core.Interfaces;
using Core.Models;
using Core.Modules.Market;
using Core.Services;
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
            ISettingsConfig config = new SettingsConfig();
            IGetSearchInstruments getSearchList = new GetSearchInstruments();
            ICalculateSyntheticQuotes syntheticCalculator = new CalculateSyntheticQuotes();

            var settings = config.ChooseSettings();
            var searchList = getSearchList.SearchInstrumentList(settings.Instruments);
            var searchListCount = searchList.Count;
            var collectedQuotesBuffer = new List<Quote>();
            var collectedQuotesBufferForDataBase = new List<Quote>();
            var streamInfo = await binanceClient.StartUserStream();
            var listenKey = streamInfo.ListenKey;
            var synthList = new List<Synthetic> { };

            while (true)
            {
                var binanceMarketInfo = await binanceClient.GetOrderBookTicker();
                var poloniexMarketInfo = await poloniexClient.GetOrderBookTicker();
                var binanceListCount = binanceMarketInfo.Count;
                var ploniexListCount = poloniexMarketInfo.Count;
                for (int i = 0; i < binanceListCount; i++)
                {
                    for (int j = 0; j < searchListCount; j++)
                    {
                        if (binanceMarketInfo[i].Symbol == searchList[j].Symbol &&
                            (searchList[j].Synthetic1 == null ||
                            searchList[j].Synthetic2 == null))
                        {
                            var exists = collectedQuotesBuffer.Any(o =>
                                o.Name == searchList[j].Symbol);
                            if (!exists)
                            {
                                var quote = new Quote
                                {
                                    Name = settings.Instruments[j].Symbol,
                                    Time = DateTime.Now.ToString(),
                                    Bid = binanceMarketInfo[i].BidPrice,
                                    Ask = binanceMarketInfo[i].AskPrice,
                                    Exchange = "Binance"
                                };
                                collectedQuotesBuffer.Add(quote);
                                collectedQuotesBufferForDataBase.Add(quote);
                            }
                        }
                        else if (binanceMarketInfo[i].Symbol == searchList[j].Synthetic1.Symbol ||
                                 binanceMarketInfo[i].Symbol == searchList[j].Synthetic2.Symbol)
                        {
                            synthList.Add(new Synthetic
                            {
                                Symbol = binanceMarketInfo[i].Symbol,
                                Bid = binanceMarketInfo[i].BidPrice,
                                Ask = binanceMarketInfo[i].AskPrice
                            });
                            if (synthList.Count == 2)
                            {
                                var quote = syntheticCalculator.Calculate(synthList);
                                collectedQuotesBuffer.Add(quote);
                                collectedQuotesBufferForDataBase.Add(quote);
                            }
                        }
                    }
                }
                System.Console.WriteLine(
                    $"Quotes count: {collectedQuotesBuffer.Count}. Press q to quit or any other to continue.");
                var input = System.Console.ReadKey().KeyChar;
                if (input == 'q')
                {
                    break;
                }
            }

            await binanceClient.CloseUserStream(listenKey);
        }
    }
}
