using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.API.Client.Interfaces;
using Core.Interfaces;
using Core.Models;
using Core.Modules.Market;

namespace Binance.API.Client.Service
{
    public class BinanceSearch: IBinanceSearch
    {
        private ICalculateSyntheticQuotes calculator;
        public BinanceSearch(ICalculateSyntheticQuotes calculator)
        {
            this.calculator = calculator;
        }
        public Task<List<Quote>> Search(List<OrderBookTicker> marketInfo, DataBaseSettings settings)
        {
            var binanceListCount = marketInfo.Count;
            var searchListCount = settings.Instruments.Count;
            var quoteList = new List<Quote> { };
            var syntheticList = new List<Synthetic> { };
            for (int i = 0; i < binanceListCount; i++)
            {
                for (int j = 0; j < searchListCount; j++)
                {
                    var searchSymbol = settings.Instruments[j].Symbol.Replace("/", "");
                    if (settings.Instruments[j].Depends == null && searchSymbol == marketInfo[i].Symbol)
                    {
                        var exists = quoteList.Any(o =>
                            o.Name == settings.Instruments[j].Symbol);
                        if (!exists)
                        {
                            var quote = new Quote
                            {
                                Name = settings.Instruments[j].Symbol,
                                Exchange = "Binance",
                                Time = DateTime.Now.ToString(),
                                Bid = marketInfo[i].BidPrice,
                                Ask = marketInfo[i].AskPrice
                            };
                            quoteList.Add(quote);
                        }
                    }
                    else if (settings.Instruments[j].Depends != null)
                    {
                        if (settings.Instruments[j].Depends.Synthetic1.Replace("/", "") ==
                            marketInfo[i].Symbol)
                        {
                            syntheticList.Add(new Synthetic
                            {
                                Symbol = settings.Instruments[j].Depends.Synthetic1,
                                Ask = marketInfo[i].AskPrice,
                                Bid = marketInfo[i].BidPrice
                            });
                        }
                        if (settings.Instruments[j].Depends.Synthetic2.Replace("/", "") ==
                            marketInfo[i].Symbol)
                        {
                            syntheticList.Add(new Synthetic
                            {
                                Symbol = settings.Instruments[j].Depends.Synthetic2,
                                Ask = marketInfo[i].AskPrice,
                                Bid = marketInfo[i].BidPrice
                            });
                        }
                    }
                }
            }

            return Task.FromResult(quoteList);
        }
    }
}
