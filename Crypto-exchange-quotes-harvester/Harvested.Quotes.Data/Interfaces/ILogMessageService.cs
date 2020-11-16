using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Harvested.Quotes.Data.Interfaces
{
    public interface ILogMessageService
    {
        Task SaveLogMessage(MessageLog message);
    }
}
