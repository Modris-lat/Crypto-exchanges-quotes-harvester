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
        public Quote Calculate(List<Synthetic> syntheticList)
        {
            return new Quote();
        }
    }
}
