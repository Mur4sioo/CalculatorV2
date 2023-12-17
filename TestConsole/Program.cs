using Evaluator;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result = CalculatorEngine.Tokenization("1+(2+3)");
            Console.WriteLine(result);
        }
    }
}