using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Poloniex.API.Client.Modules;

namespace Poloniex.API.Client.Interfaces
{
    public interface IPoloniexSearch
    {
        Task<List<Quote>> Search(List<PoloniexMarketData> marketInfo, List<SearchInstrument> searchList);
    }
}
