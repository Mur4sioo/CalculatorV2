using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Evaluator
{
    public class CalculatorEngine
    {
        private static readonly char[] Operators = { '+', '-', '*', '/' };

        public double Evaluate(string math, CultureInfo? culture = null)
        {
            var ast = Parser.ParseExpression(math, culture);
            return Math.Round(ast.Evaluate(),2);
            
        }
        public static List<Token> Tokenization(string math, CultureInfo? culture = null)
        {
            var list = new List<Token>();
            var lexer = new Lexer(math, culture);
            do
            {
                list.Add(lexer.Current);
            } while (lexer.Read());
            return list;
        }
        
    }

}

