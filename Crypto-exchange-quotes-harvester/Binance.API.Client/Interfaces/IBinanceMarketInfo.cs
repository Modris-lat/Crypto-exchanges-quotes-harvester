using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Binance.API.Client
{
    public interface IBinanceMarketInfo
    {
        IBinanceClient BinanceClient { get; }
        Task GetBinanceMarketInfo();
    }
}
