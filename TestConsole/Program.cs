using System.Globalization;
using Evaluator;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var i = 0;
            var result = CalculatorEngine.Tokenization("1+2+3", CultureInfo.CurrentCulture);
            foreach (var t in result)
            {
                Console.WriteLine(result[i]);
                i++;
            }
        }
    }
}