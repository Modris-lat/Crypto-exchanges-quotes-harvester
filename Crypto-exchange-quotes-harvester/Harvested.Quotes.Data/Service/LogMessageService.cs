using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
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
        public async Task SaveLogMessage(MessageLog message)
        {
            _ctx.MessageLogs.Add(message);
            await _ctx.SaveChangesAsync();
        }
    }
}
