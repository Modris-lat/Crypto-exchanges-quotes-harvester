using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace Core.Services
{
    public class CalculateSyntheticQuotes: ICalculateSyntheticQuotes
    {
        public Quote Calculate(List<Synthetic> syntheticList, DataBaseSettings settings)
        {
            var quote = new Quote();
            for (int i = 0; i < settings.Instruments.Count; i++)
            {
                if (settings.Instruments[i].Depends != null)
                {
                    if (settings.Instruments[i].Depends.Synthetic1 == syntheticList[0].Symbol &&
                        settings.Instruments[i].Depends.Synthetic2 == syntheticList[1].Symbol)
                    {
                        quote.Time = DateTime.Now.ToString();
                        quote.Name = settings.Instruments[i].Symbol;
                        quote.Bid = Math.Ceiling(syntheticList[0].Bid / syntheticList[1].Bid);
                        quote.Ask = Math.Ceiling(syntheticList[0].Ask / syntheticList[1].Ask);
                        quote.Exchange = syntheticList[0].Exchange;
                        return quote;
                    }
                    else if (settings.Instruments[i].Depends.Synthetic1 == syntheticList[1].Symbol &&
                             settings.Instruments[i].Depends.Synthetic2 == syntheticList[0].Symbol)
                    {
                        quote.Time = DateTime.Now.ToString();
                        quote.Name = settings.Instruments[i].Symbol;
                        quote.Bid = Math.Ceiling(syntheticList[1].Bid / syntheticList[0].Bid);
                        quote.Ask = Math.Ceiling(syntheticList[1].Ask / syntheticList[0].Ask);
                        quote.Exchange = syntheticList[0].Exchange;
                        return quote;
                    }
                }
            }
            return quote;
        }
    }
}
