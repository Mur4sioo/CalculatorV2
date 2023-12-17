using System.Reflection.Metadata.Ecma335;

namespace Evaluator
{
    public class CalculatorEngine
    {
        private static readonly char[] Operators = { '+', '-', '*', '/' };
        public double Evaluate(string math)
        {
            var ast = Parser.ParseExpression(math);
            return ast.Evaluate();
        }
        public static List<Token> Tokenization(string math)
        {
            List<Token> Tokens = new List<Token>();
            var operatorindex = math.IndexOfAny(Operators);
            while (operatorindex >= 0)
            {
                var numberpart = math.Substring(0, operatorindex);
                Tokens.Add(new Token(TokenType.Number, Convert.ToDouble(numberpart)));
                var operatorcharacter = math[operatorindex];
                switch (operatorcharacter)
                {
                    case '+':
                        Tokens.Add(new Token(TokenType.OperatorPlus, 0));
                        break;
                    case '-':
                        Tokens.Add(new Token(TokenType.OperatorMinus, 0));
                        break;
                    case '*':
                        Tokens.Add(new Token(TokenType.OperatorMultiply, 0));
                        break;
                    case '/':
                        Tokens.Add(new Token(TokenType.OperatorDivide, 0));
                        break;
                }

                math = math.Substring(operatorindex + 1);
                operatorindex = math.IndexOfAny(Operators);
            }

            if (math.Length > 0)
            {
                Tokens.Add(new Token(TokenType.Number, Convert.ToDouble(math)));
            }

            return Tokens;
        }
    }

    public class Lexer
    {
        private readonly string text;
        private int index;
        public Lexer(string text)
        {
            this.text = text;
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
        public Token Current { get; private set; }
        public bool IsFinished => index >= this.text.Length;
        public string GetRemainingText() => this.text.Substring(index);
        private static bool IsNumberOrDecimal(char ch) => ch == '.' || char.IsAsciiDigit(ch);

        private static int CountNumberOrDecimal(string text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                if (IsNumberOrDecimal(text[i]) is false) 
                    return i;
            }
            return text.Length;
        }
        public bool Read()
        {
            if (IsFinished)
                return false;
            var remaining = GetRemainingText();
            var firstCharacter = remaining[0];
            if (singleCharTokens.TryGetValue(firstCharacter, out var tokenType))
            {
                this.Current = new Token(tokenType, 0);
                index++;
                return true;
            }

            if (IsNumberOrDecimal(firstCharacter))
            {
                var length = CountNumberOrDecimal(remaining);
                var numberPart = remaining.Substring(0, length);
                this.Current = new Token(TokenType.Number, double.Parse(numberPart));
                index += length;
            }
            throw new NotImplementedException();
        }
    }
    public class Parser
    {
        private readonly List<Token> tokens;
        private int index;
        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            this.index = 0;
        }

        public static AstNode? ParseExpression(string input)
        {
            var tokens = CalculatorEngine.Tokenization(input);
            var parser = new Parser(tokens);
            var result = parser.ParseExpression();
            if (result is null)
                return null;
            if (parser.IsFinished() is false)
                throw new Exception("Incomplete parse");
            return result;
        }

        /*
        For reference, here are the rules that pertain to tokenization
         
        Additive_Operator
            : '+'   // TokenType: OperatorAdd
            | '-'   // TokenType: OperatorSubtract
            ;
        Multiplicative_Operator
            : '*'   // TokenType: OperatorMultiply
            | '/'   // TokenType: OperatorDivide
            ;
        Number_Literal
            : Digit+              // TokenType: Number
            | Digit+ '.' Digit+   // TokenType: Number
            |        '.' Digit+   // TokenType: Number
            | Digit+ '.'          // TokenType: Number
            ;
        Digit : '0'..'9'          // Not a token.  This rule is only used as a short-hand when defining other rules
        */


        #region Parse Methods

        private AstNode? ParseExpression()
        {
            /*
            expression : additive ;
            1+2
            */


            return ParseAdditive();
        }

        private AstNode? ParseAdditive()
        {
            /*
            additive
                : multiplicative ( Additive_Operator multiplicative )*
                ;
            */
            var left = ParseMultiplicative();
            while (TryConsumeTokenType(TokenType.OperatorPlus, TokenType.OperatorMinus, out var foundTokenType))
            {
                var right = ParseMultiplicative();
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
            throw new NotImplementedException();
        }
        private AstNode? ParseMultiplicative()
        {
            /*
            multiplicative
                : number ( Multiplicative_Operator number )*
                ;
            */
            var left = ParseNumber();
            while (TryConsumeTokenType(TokenType.OperatorMultiply, TokenType.OperatorDivide, out var foundTokenType))
            {
                var right = ParseNumber();
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

        private AstNode? ParseNumber()
        {
            // number : Number_Literal ; 
            if (TryConsumeNumber(out var number))
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


        #region Helper Methods

        private bool IsFinished()
        {
            return this.index >= tokens.Count;
        }

        private bool TryConsumeTokenType(TokenType tokenType, out double number)
        {
            /* TODO: Implement this method
             * If we're already finished, return false.
             * If the current token's TokenType is not equal to the tokenType parameter, return false.
             * Otherwise,
             *      Set the "number" out parameter to the "Number" property of the current token
             *      Increment index, so that we point to the next token.
             *      Return true
             */
            number = 0;
            if (IsFinished())
            {
                return false;
            }

            if (tokens[index].TokenType != tokenType)
            {
                return false;
            }
            else
            {
                number = tokens[index].Number;
                index++;
                return true;
            }
            throw new NotImplementedException();
        }
        private bool TryConsumeTokenType(TokenType tokenType)
        {
            return TryConsumeTokenType(tokenType, out _);
        }
        private bool TryConsumeNumber(out double number)
        {
            return TryConsumeTokenType(TokenType.Number, out number);
        }

        /// <summary>
        /// Attempts to consume one of two different token types
        /// </summary>
        /// <param name="a">The first option</param>
        /// <param name="b">The second option</param>
        /// <param name="found">
        /// Contains the token type that was found - either <paramref name="a"/> or <paramref name="b"/>
        /// </param>
        /// <returns>
        /// Returns true if one of the token types was consumed.
        /// </returns>
        private bool TryConsumeTokenType(TokenType a, TokenType b, out TokenType found)
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

        #endregion Helper Methods
    }
}

