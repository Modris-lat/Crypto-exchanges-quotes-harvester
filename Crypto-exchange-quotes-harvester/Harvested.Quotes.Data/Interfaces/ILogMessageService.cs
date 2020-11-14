using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvested.Quotes.Data.Interfaces
{
    public interface ILogMessageService
    {
        Task SaveLogMessage(string message);
    }
}
