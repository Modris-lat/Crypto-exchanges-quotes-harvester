using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core
{
    public interface IGetSearchInstruments
    {
        List<SearchInstrument> SearchInstrumentList(List<Instrument> instruments);
    }
}
