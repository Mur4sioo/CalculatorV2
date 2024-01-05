using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Evaluator
{
    public class CalculatorEngine
    {

        public double Evaluate(string math, IReadOnlyDictionary<string, char>? variables= null, CultureInfo? culture = null)
        {
            var ast = Parser.ParseExpression(math, culture);
            return Math.Round(ast.Evaluate(variables),2);
            
        }

        public double Evaluate(string math, CultureInfo culture, IReadOnlyDictionary<string, char>? variables = null)
        {
            return Evaluate(math, culture, variables);
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

