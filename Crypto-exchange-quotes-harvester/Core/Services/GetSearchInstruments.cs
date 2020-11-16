using System.Collections.Generic;
using Core.Models;

namespace Core.Services
{
    public class GetSearchInstruments: IGetSearchInstruments
    {
        public List<SearchInstrument> SearchInstrumentList(List<Instrument> instruments)
        {
            var list = new List<SearchInstrument>() { };
            for (int i = 0; i < instruments.Count; i++)
            {
                if (instruments[i].Depends == null)
                {
                    var instrument = new SearchInstrument { Symbol = instruments[i].Symbol.Replace("/", "") };
                    list.Add(instrument);
                }
                else
                {
                    var instrument = new SearchInstrument
                    {
                        Symbol = instruments[i].Symbol.Replace("/", ""),
                        Synthetic1 = new Synthetic
                        {
                            SearchName = instruments[i].Depends.Synthetic1.Replace("/", ""), 
                            Symbol = instruments[i].Depends.Synthetic1
                        },
                        Synthetic2 = new Synthetic
                        {
                            SearchName = instruments[i].Depends.Synthetic2.Replace("/", ""), 
                            Symbol = instruments[i].Depends.Synthetic2
                        }
                    };
                    list.Add(instrument);
                }
            }

            return list;
        }
    }
}
