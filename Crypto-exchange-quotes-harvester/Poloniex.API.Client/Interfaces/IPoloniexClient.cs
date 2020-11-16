using System.Collections.Generic;
using System.Threading.Tasks;
using Poloniex.API.Client.Modules;

namespace Poloniex.API.Client
{
    public interface IPoloniexClient
    {
        Task<List<PoloniexMarketData>> GetOrderBookTicker();
    }
}
