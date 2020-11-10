using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Poloniex.API.Client.Modules;

namespace Poloniex.API.Client
{
    public interface IPoloniexClient
    {
        Task<IEnumerable<PoloniexMarketData>> GetOrderBookTicker();
    }
}
