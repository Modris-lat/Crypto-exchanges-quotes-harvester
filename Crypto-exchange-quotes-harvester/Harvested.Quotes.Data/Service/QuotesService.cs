using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Harvested.Quotes.Data.Interfaces;

namespace Harvested.Quotes.Data.Service
{
    public class QuotesService: IQuoteService
    {
        private IQuotesDBContext _ctx;

        public QuotesService(IQuotesDBContext ctx)
        {
            _ctx = ctx;
        }
        public async Task SaveQuotes(List<Quote> quoteList)
        {
            for (int i = 0; i < quoteList.Count; i++)
            {
                _ctx.Quotes.Add(quoteList[i]);
            }
            await _ctx.SaveChangesAsync();
        }

        public async Task ClearData()
        {
            _ctx.Quotes.RemoveRange(_ctx.Quotes);
            await _ctx.SaveChangesAsync();
        }
    }
}
