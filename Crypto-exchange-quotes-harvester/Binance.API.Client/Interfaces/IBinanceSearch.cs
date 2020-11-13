using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Modules.Market;

namespace Binance.API.Client.Interfaces
{
    public interface IBinanceSearch
    {
        Task<List<Quote>> Search(List<OrderBookTicker> marketInfo, DataBaseSettings settings);
    }
}
