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
        public decimal Calculate(decimal number1, decimal number2)
        {
            return number1 / number2;
        }
    }
}
