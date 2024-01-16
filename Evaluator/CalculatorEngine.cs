using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Evaluator
{
    public class CalculatorEngine
    {

        public double Evaluate(string math, IReadOnlyDictionary<string, double>? variables= null, ExpressionOptions? options = null)
        {
            var ast = Parser.ParseExpression(math, options);
            return Math.Round(ast.Evaluate(variables),2);
            
        }

        public double Evaluate(string math, ExpressionOptions options, IReadOnlyDictionary<string, double>? variables = null)
        {
            return Evaluate(math, variables, options);
        }
        public static List<Token> Tokenization(string math, ExpressionOptions? options = null)
        {
            options ??= ExpressionOptions.Default;
            var list = new List<Token>();
            var lexer = new Lexer(math, options);
            do
            {
                list.Add(lexer.Current);
            } while (lexer.Read());
            return list;
        }
        
    }

}

