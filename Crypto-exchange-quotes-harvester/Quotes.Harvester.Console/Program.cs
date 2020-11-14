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
using Poloniex.API.Client.Interfaces;
using Poloniex.API.Client.Service;

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
            ICalculateSyntheticQuotes syntheticCalculator = new CalculateSyntheticQuotes();
            IBinanceSearch binanceSearchMethod = new BinanceSearch(syntheticCalculator);
            IGetSearchInstruments createSearchInstruments = new GetSearchInstruments();
            IPoloniexSearch poloniexSearchMethod = new PoloniexSearch(syntheticCalculator);

            var settings = config.ChooseSettings();
            var searchList = createSearchInstruments.SearchInstrumentList(settings.Instruments);
            var streamInfo = await binanceClient.StartUserStream();
            var listenKey = streamInfo.ListenKey;

            while (true)
            {
                System.Console.WriteLine("Getting data...");
                var binanceMarketInfo = await binanceClient.GetOrderBookTicker();
                var poloniexMarketInfo = await poloniexClient.GetOrderBookTicker();
                var quotesBinance = await binanceSearchMethod.Search(binanceMarketInfo, searchList);
                var quotesPoloniex = await poloniexSearchMethod.Search(poloniexMarketInfo, searchList);

                System.Console.WriteLine($"Poloniex quotes count {quotesPoloniex.Count}");
                System.Console.WriteLine(
                    $"Binance quotes count: {quotesBinance.Count}. Press q to quit or any other to continue.");
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
