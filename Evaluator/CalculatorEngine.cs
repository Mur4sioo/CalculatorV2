using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Evaluator
{
    public class CalculatorEngine
    {
        private static readonly char[] Operators = { '+', '-', '*', '/' };

        public double Evaluate(string math)
        {
            var ast = Parser.ParseExpression(math);
            return Math.Round(ast.Evaluate(),2);
            
        }
        public static List<Token> Tokenization(string math)
        {
            var list = new List<Token>();
            var lexer = new Lexer(math);
            do
            {
                list.Add(lexer.Current);
            } while (lexer.Read());
            return list;
        }
        
    }

}

