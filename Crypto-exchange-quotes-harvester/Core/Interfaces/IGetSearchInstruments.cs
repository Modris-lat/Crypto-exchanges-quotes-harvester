using System.Collections.Generic;
using Core.Models;

namespace Core
{
    public interface IGetSearchInstruments
    {
        List<SearchInstrument> SearchInstrumentList(List<Instrument> instruments);
    }
}
