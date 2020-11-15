using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Binance.API.Client;
using Binance.API.Client.Interfaces;
using Binance.API.Client.Service;
using Core;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Harvested.Quotes.Data;
using Harvested.Quotes.Data.Interfaces;
using Harvested.Quotes.Data.Service;
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
            IQuotesDBContext context = new QuotesDBContext();
            IQuoteService quotesStorage = new QuotesService(context);
            ILogMessageService messageService = new LogMessageService(context);

            var stopWatch = new Stopwatch();
            var settings = config.ChooseSettings();
            var searchList = createSearchInstruments.SearchInstrumentList(settings.Instruments);
            var streamInfo = await binanceClient.StartUserStream();
            var listenKey = streamInfo.ListenKey;
            var quotesBuffer = new List<Quote>() { };
            await quotesStorage.ClearData();
            stopWatch.Start();
            while (true)
            {
                System.Console.WriteLine("Getting data...");
                try
                {
                    var binanceMarketInfo = await binanceClient.GetOrderBookTicker();
                    var quotesBinance = await binanceSearchMethod.Search(binanceMarketInfo, searchList);
                    if (quotesBinance.Any())
                    {
                        quotesBuffer.AddRange(quotesBinance);
                    }
                }
                catch
                {
                    await messageService.SaveLogMessage(new MessageLog{Message = $"Binance server error {DateTime.Now}"});
                    System.Console.WriteLine("Binance Server Error!");
                    continue;
                }

                try
                {
                    var poloniexMarketInfo = await poloniexClient.GetOrderBookTicker();
                    var quotesPoloniex = await poloniexSearchMethod.Search(poloniexMarketInfo, searchList);
                    if (quotesPoloniex.Any())
                    {
                        quotesBuffer.AddRange(quotesPoloniex);
                    }
                }
                catch
                {
                    await messageService.SaveLogMessage(new MessageLog { Message = $"Poloniex server error {DateTime.Now}" });
                    System.Console.WriteLine("Poloniex Server Error!");
                    continue;
                }
                
                if (stopWatch.ElapsedMilliseconds >= settings.FlushPeriod)
                {
                    await quotesStorage.SaveQuotes(quotesBuffer);
                    stopWatch.Reset();
                    quotesBuffer.Clear();
                }

                System.Console.WriteLine(
                    $"Binance quotes count: {quotesBuffer.Count}. Press q to quit or any other to continue.");
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
