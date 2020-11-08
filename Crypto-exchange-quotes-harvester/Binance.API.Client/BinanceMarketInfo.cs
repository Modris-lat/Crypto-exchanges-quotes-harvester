using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Binance.API.Client
{
    public class BinanceMarketInfo: IBinanceMarketInfo
    {
        public BinanceMarketInfo(IBinanceClient binanceClient)
        {
            BinanceClient = binanceClient;
        }

        public IBinanceClient BinanceClient { get; }
        public async Task GetBinanceMarketInfo()
        {
            var orderBookList = await BinanceClient.GetOrderBookTicker();
            var marketInfo = orderBookList.ToList();
            for (int i = 0; i < marketInfo.Count(); i++)
            {
                if (marketInfo[i].Symbol == "ETHBTC")
                {
                    Console.WriteLine(marketInfo[i].AskPrice);
                    Console.WriteLine(marketInfo[i].BidPrice);
                    Console.WriteLine(marketInfo[i].Symbol);
                }
            }
        }
    }
}
