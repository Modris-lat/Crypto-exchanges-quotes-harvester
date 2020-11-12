﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Binance.API.Client;
using Core;
using Core.Interfaces;
using Core.Models;
using Poloniex.API.Client;

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
            ISettingsConfig settings = new DataBaseSettings();
            settings.ChooseSettings();
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
                            Console.WriteLine("Binance market:");
                            Console.WriteLine(currencyOrderList[i].Symbol);
                            Console.WriteLine(currencyOrderList[i].AskPrice);
                            Console.WriteLine(currencyOrderList[i].BidPrice);
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
                            Console.WriteLine("Poloniex market:");
                            Console.WriteLine($"{poloniexList[i].Symbol}");
                            Console.WriteLine($"{poloniexList[i].AskPrice}");
                            Console.WriteLine($"{poloniexList[i].BidPrice}");
                            Console.WriteLine($"{poloniexList[i].IsFrozen}");
                        }
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
