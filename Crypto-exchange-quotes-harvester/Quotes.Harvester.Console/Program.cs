using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Binance.API.Client.Interfaces;
using Core;
using Core.Interfaces;
using Core.Models;
using Harvested.Quotes.Data.Interfaces;
using Poloniex.API.Client;
using Poloniex.API.Client.Interfaces;

namespace Quotes.Harvester.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IBinanceClient binanceClient = DependencyFactory.CreateBinanceClient();
            IPoloniexClient poloniexClient = DependencyFactory.CreatePoloniexClient();
            ISettingsConfig config = DependencyFactory.CreateSettingsConfig();
            IBinanceSearch binanceSearchMethod = DependencyFactory.CreateBinanceSearch();
            IGetSearchInstruments createSearchInstruments = DependencyFactory.CreateSearchInstruments();
            IPoloniexSearch poloniexSearchMethod = DependencyFactory.CreatePoloniexSearch();
            IQuoteService quotesStorage = DependencyFactory.CreateQuoteService();
            ILogMessageService messageService = DependencyFactory.CreateLogMessageService();

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
                catch(Exception e)
                {
                    await messageService.SaveLogMessage(new MessageLog{Message = $"Binance server error: {e.Message} {DateTime.Now}"});
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
                catch(Exception e)
                {
                    await messageService.SaveLogMessage(new MessageLog { Message = $"Poloniex server error: {e.Message} {DateTime.Now}" });
                    System.Console.WriteLine("Poloniex Server Error!");
                    continue;
                }
                
                if (stopWatch.ElapsedMilliseconds >= settings.FlushPeriod)
                {
                    await quotesStorage.SaveQuotes(quotesBuffer);
                    stopWatch.Restart();
                    quotesBuffer.Clear();
                }

                System.Console.WriteLine(
                    $"Collected quotes count: {quotesBuffer.Count}. Press q to quit or any other to continue.");
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
