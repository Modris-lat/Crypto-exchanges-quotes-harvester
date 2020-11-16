using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Modules.Market;

namespace Binance.API.Client.Interfaces
{
    public interface IBinanceSearch
    {
        Task<List<Quote>> Search(List<OrderBookTicker> marketInfo, List<SearchInstrument> searchList);
    }
}
