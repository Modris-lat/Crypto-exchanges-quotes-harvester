using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    var item = instruments[i].Symbol.Split('/');
                    var instrument = new SearchInstrument { Symbol = item[0] + item[1] };
                    list.Add(instrument);
                }
                else
                {
                    var item = instruments[i].Symbol.Split('/');
                    var synthetic1 = instruments[i].Depends.Synthetic1.Split('/');
                    var synthetic2 = instruments[i].Depends.Synthetic2.Split('/');
                    var instrument = new SearchInstrument
                    {
                        Symbol = instruments[i].Symbol,
                        Synthetic1 = new Synthetic
                        {
                            SearchName = synthetic1[0] + synthetic1[1], 
                            Symbol = instruments[i].Depends.Synthetic1
                        },
                        Synthetic2 = new Synthetic
                        {
                            SearchName = synthetic2[0] + synthetic2[1], 
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
