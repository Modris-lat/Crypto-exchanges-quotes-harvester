using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harvested.Quotes.Data.Interfaces;

namespace Harvested.Quotes.Data.Service
{
    public class LogMessageService: ILogMessageService
    {
        private IQuotesDBContext _ctx;

        public LogMessageService(IQuotesDBContext ctx)
        {
            _ctx = ctx;
        }
        public Task SaveLogMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
