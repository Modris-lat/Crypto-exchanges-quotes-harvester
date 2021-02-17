using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Harvested.Quotes.Data.Interfaces
{
    public interface IQuoteService
    {
        Task SaveQuotes(List<Quote> quoteList);
        Task ClearData();
    }
}
