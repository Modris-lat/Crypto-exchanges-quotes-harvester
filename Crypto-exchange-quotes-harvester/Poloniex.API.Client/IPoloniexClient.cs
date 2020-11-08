using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Modules.Market;

namespace Poloniex.API.Client
{
    public interface IPoloniexClient
    {
        Task<dynamic> TestConnectivity();
        Task<IEnumerable<OrderBookTicker>> GetOrderBookTicker();
    }
}
