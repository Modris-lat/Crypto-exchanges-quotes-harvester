using Core.Interfaces;

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
