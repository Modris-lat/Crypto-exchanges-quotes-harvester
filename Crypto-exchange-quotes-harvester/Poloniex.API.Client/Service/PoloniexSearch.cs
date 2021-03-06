﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Poloniex.API.Client.Interfaces;
using Poloniex.API.Client.Modules;

namespace Poloniex.API.Client.Service
{
    public class PoloniexSearch : IPoloniexSearch
    {
        private ICalculateSyntheticQuotes calculator;

        public PoloniexSearch(ICalculateSyntheticQuotes calculator)
        {
            this.calculator = calculator;
        }

        public Task<List<Quote>> Search(List<PoloniexMarketData> marketInfo, List<SearchInstrument> searchList)
        {
            var binanceListCount = marketInfo.Count;
            var searchListCount = searchList.Count;
            var quoteList = new List<Quote> { };
            for (int i = 0; i < binanceListCount; i++)
            {
                for (int j = 0; j < searchListCount; j++)
                {
                    var exists = quoteList.Any(o =>
                        o.Name == searchList[j].Symbol);
                    if (!exists)
                    {
                        if (searchList[j].Synthetic1 == null && searchList[j].Synthetic2 == null &&
                            searchList[j].Symbol == marketInfo[i].Symbol)
                        {
                            var quote = CreateQuote(searchList[j].Symbol, marketInfo[i].BidPrice, marketInfo[i].AskPrice);
                            quoteList.Add(quote);
                        }
                        else if (searchList[j].Synthetic1 != null && searchList[j].Synthetic2 != null)
                        {
                            if (searchList[j].Synthetic1.SearchName == marketInfo[i].Symbol)
                            {
                                var synthetic = marketInfo[i];
                                searchList[j].Synthetic1.Ask = synthetic.AskPrice;
                                searchList[j].Synthetic1.Bid = synthetic.BidPrice;
                                searchList[j].Synthetic1.Exchange = "Poloniex";
                            }

                            if (searchList[j].Synthetic2.SearchName == marketInfo[i].Symbol)
                            {
                                var synthetic = marketInfo[i];
                                searchList[j].Synthetic2.Ask = synthetic.AskPrice;
                                searchList[j].Synthetic2.Bid = synthetic.BidPrice;
                                searchList[j].Synthetic2.Exchange = "Poloniex";
                            }
                        }
                    }
                }
            }

            foreach (var item in searchList)
            {
                if (item.Synthetic1 != null && item.Synthetic2 != null)
                {
                    if (item.Synthetic1.Ask > 0 && item.Synthetic2.Ask > 0)
                    {
                        var ask = calculator.Calculate(item.Synthetic2.Ask, item.Synthetic1.Ask);
                        var bid = calculator.Calculate(item.Synthetic2.Bid, item.Synthetic1.Bid);
                        var quote = CreateQuote(item.Symbol, bid, ask);
                        quoteList.Add(quote);
                    }
                }
            }

            return Task.FromResult(quoteList);
        }

        public Quote CreateQuote(string name, decimal bid, decimal ask)
        {
            var quote = new Quote
            {
                Name = name,
                Exchange = "Poloniex",
                Time = DateTime.Now.ToString(),
                Bid = bid,
                Ask = ask
            };
            return quote;
        }
    }
}
