using System.Globalization;
using Evaluator;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach (var x in FunctionList.Functions)
            {
                Console.WriteLine(x);
            }
        }
    }
}