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

        //public static List<Token> Tokenizationk(string math)
        //{
        //    List<Token> Tokens = new List<Token>();
        //    var operatorindex = math.IndexOfAny(Operators);
        //    while (operatorindex >= 0)
        //    {
        //        var numberpart = math.Substring(0, operatorindex);
        //        Tokens.Add(new Token(TokenType.Number, Convert.ToDouble(numberpart)));
        //        var operatorcharacter = math[operatorindex];
        //        switch (operatorcharacter)
        //        {
        //            case '+':
        //                Tokens.Add(new Token(TokenType.OperatorPlus, 0));
        //                break;
        //            case '-':
        //                Tokens.Add(new Token(TokenType.OperatorMinus, 0));
        //                break;
        //            case '*':
        //                Tokens.Add(new Token(TokenType.OperatorMultiply, 0));
        //                break;
        //            case '/':
        //                Tokens.Add(new Token(TokenType.OperatorDivide, 0));
        //                break;
        //        }

        //        math = math.Substring(operatorindex + 1);
        //        operatorindex = math.IndexOfAny(Operators);
        //    }

        //    if (math.Length > 0)
        //    {
        //        Tokens.Add(new Token(TokenType.Number, Convert.ToDouble(math)));
        //    }

        //    return Tokens;
        //}

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

    public class Lexer
    {
        private readonly string text;
        private int index;
        public Lexer(string text)
        {
            this.text = text;
            Read();
        }

        private static readonly Dictionary<char, TokenType> singleCharTokens = new Dictionary<char, TokenType>()
        {
            { '(', TokenType.ParenOpen },
            { ')', TokenType.ParenClose },
            { '+', TokenType.OperatorPlus },
            { '-', TokenType.OperatorMinus },
            { '*', TokenType.OperatorMultiply },
            { '/', TokenType.OperatorDivide },
        };
        public Token Current { get; private set; } = Token.Unknown;
        public bool IsFinished { get; private set; }
        public ReadOnlySpan<char> GetRemainingText() => this.text.AsSpan().Slice(index);
        private static bool IsNumberOrDecimal(char ch) => ch == '.' || char.IsAsciiDigit(ch);

        private static int CountNumberOrDecimal(ReadOnlySpan<char> text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                if (IsNumberOrDecimal(text[i]) is false) 
                    return i;
            }
            return text.Length;
        }

        private void SkipWhiteSpace()
        {
            var count = CountWhiteSpaceCharacters(GetRemainingText());
            index += count;
        }

        private static int CountWhiteSpaceCharacters(ReadOnlySpan<char> text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                if (char.IsWhiteSpace(text[i]) is false)
                    return i;
            }
            return text.Length;
        }
        public bool Read()
        {
            if (IsFinished)
            {
                this.Current = Token.Unknown;
                return false;
            }

            SkipWhiteSpace();
            var remaining = GetRemainingText();
            if (remaining.IsEmpty)
            {
                this.IsFinished = true;
                this.Current = Token.Unknown;
                return false;
            }
            var firstCharacter = remaining[0];
            if (singleCharTokens.TryGetValue(firstCharacter, out var tokenType) is true)
            {
                this.Current = new Token(tokenType, 0);
                index++;
                return true;
            }

            if (IsNumberOrDecimal(firstCharacter))
            {
                var length = CountNumberOrDecimal(remaining);
                var numberPart = remaining.Slice(0, length);
                this.Current = new Token(TokenType.Number, double.Parse(numberPart));
                index += length;
                return true;
            }
            
            return false;

            throw new NotImplementedException();
        }
        public bool TryConsumeTokenType(TokenType tokenType, out double number)
        {
            number = 0;
            if (IsFinished || this.Current.TokenType != tokenType)
            {
                return false;
            }
            number = this.Current.Number;
            Read();
            return true;
        }
        public bool TryConsumeTokenType(TokenType tokenType)
        {
            return TryConsumeTokenType(tokenType, out _);
        }
        public bool TryConsumeNumber(out double number)
        {
            return TryConsumeTokenType(TokenType.Number, out number);
        }
        public bool TryConsumeTokenType(TokenType a, TokenType b, out TokenType found)
        {
            if (TryConsumeTokenType(a))
            {
                found = a;
                return true;
            }

            if (TryConsumeTokenType(b))
            {
                found = b;
                return true;
            }

            found = default;
            return false;
            throw new NotImplementedException();
        }
    }
    public class Parser
    {
        private readonly  Lexer lexer;
        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public static AstNode ParseExpression(string input)
        {
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            var result = parser.ParseExpression();
            if (result is null)
                throw new Exception("Invalid Input");
            if (lexer.IsFinished is false)
                throw new Exception("Incomplete parse");
            return result;
        }
        #region Parse Methods

        private AstNode? ParseExpression()
        {
            return ParseAdditive();
        }

        private AstNode? ParseAdditive()
        {
            var left = ParseMultiplicative();
            if (left is null)
                return null;
            while (lexer.TryConsumeTokenType(TokenType.OperatorPlus, TokenType.OperatorMinus, out var foundTokenType))
            {
                var right = ParseMultiplicative();
                if (right is null)
                    return null;
                switch (foundTokenType)
                {
                    case TokenType.OperatorPlus:
                        left = new BinaryNode(left, BinaryOperator.Add, right);
                        break;
                    case TokenType.OperatorMinus:
                        left = new BinaryNode(left, BinaryOperator.Subtract, right);
                        break;
                    default:
                        return left;
                }
            }
            return left;
        }
        private AstNode? ParseMultiplicative()
        {
            var left = ParsePrimary();
            if (left is null)
                return null;
            while (lexer.TryConsumeTokenType(TokenType.OperatorMultiply, TokenType.OperatorDivide, out var foundTokenType))
            {
                var right = ParsePrimary();
                if (right is null)
                    throw new Exception($"Invalid parse: had a {foundTokenType}, but could not parse a number");
                switch (foundTokenType)
                {
                    case TokenType.OperatorMultiply:
                         left = new BinaryNode(left, BinaryOperator.Multiply, right);
                        break;
                    case TokenType.OperatorDivide:
                        left = new BinaryNode(left, BinaryOperator.Divide, right);
                        break;
                    default:
                        return left;
                }
            }
            return left;
            throw new NotImplementedException();
        }

        private AstNode? ParsePrimary()
        {
            return ParseParenthetical() ?? ParseNumber();
        }

        private AstNode? ParseParenthetical()
        {
            if (lexer.TryConsumeTokenType((TokenType.ParenOpen)))
            {
                var left = ParseExpression();
                if (left is null)
                    throw new Exception("Bad Formula.");
                if (lexer.TryConsumeTokenType(TokenType.ParenClose))
                    return left;
                throw new Exception("Bad Formula.");
            }
            return null;
        }
        private AstNode? ParseNumber()
        {
            if (lexer.TryConsumeNumber(out var number))
            {
                return new NumberNode(number);
            }
            else
            {
                return null;
            }
            throw new NotImplementedException();
        }

        #endregion Parse Methods
    }
}

