using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Task SaveQuotes(List<Quote> quoteList)
        {
            throw new NotImplementedException();
        }
    }
}
