using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.API.Client;
using Binance.API.Client.Interfaces;
using Binance.API.Client.Service;
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
            IBinanceSearch searchMethod = new BinanceSearch(syntheticCalculator);

            var settings = config.ChooseSettings();
            var searchList = getSearchList.SearchInstrumentList(settings.Instruments);
            var collectedQuotesBuffer = new List<Quote>();
            var collectedQuotesBufferForDataBase = new List<Quote>();
            var streamInfo = await binanceClient.StartUserStream();
            var listenKey = streamInfo.ListenKey;
            var synthList = new List<Synthetic> { };

            while (true)
            {
                var binanceMarketInfo = await binanceClient.GetOrderBookTicker();
                var poloniexMarketInfo = await poloniexClient.GetOrderBookTicker();
                var ploniexListCount = poloniexMarketInfo.Count;
                var quotes = await searchMethod.Search(binanceMarketInfo, settings);

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
