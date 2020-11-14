using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Poloniex.API.Client.Interfaces;
using Poloniex.API.Client.Modules;

namespace Poloniex.API.Client.Service
{
    public class PoloniexSearch: IPoloniexSearch
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
                            var quote = new Quote
                            {
                                Name = searchList[j].Symbol,
                                Exchange = "Poloniex",
                                Time = DateTime.Now.ToString(),
                                Bid = marketInfo[i].BidPrice,
                                Ask = marketInfo[i].AskPrice
                            };
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

                            if (searchList[j].Synthetic1.Ask > 0 && searchList[j].Synthetic2.Ask > 0)
                            {
                                var synthetic = searchList[j];
                                var ask = calculator.Calculate(synthetic.Synthetic2.Ask, synthetic.Synthetic1.Ask);
                                var bid = calculator.Calculate(synthetic.Synthetic2.Bid, synthetic.Synthetic1.Bid);
                                var quote = new Quote
                                {
                                    Name = synthetic.Symbol,
                                    Exchange = "Poloniex",
                                    Time = DateTime.Now.ToString(),
                                    Bid = bid,
                                    Ask = ask
                                };
                                quoteList.Add(quote);
                            }
                        }
                    }
                }
            }

            return Task.FromResult(quoteList);
        }
    }
}
