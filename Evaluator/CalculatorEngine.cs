namespace Evaluator
{
    public class CalculatorEngine
    {
        static readonly char[] Operators = { '+', '-', '*', '/' };

        public double Evaluate(string math)
        {
            var infixTokens = Tokenization(math);
            var postfixTokens = ShuntingYard(infixTokens);
            var result = Evaluation(postfixTokens);
            return result;
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

        private static List<Token> ShuntingYard(List<Token> tokens)
        {
            List<Token> outputList = new List<Token>();
            List<Token> tokensOperators = new List<Token>();
            if (tokens.Count > 0)
            {
                foreach (var t in tokens)
                {
                    if (t.TokenType == TokenType.Number)
                    {
                        outputList.Add(t);
                    }
                    if (t.TokenType != TokenType.Number)
                    {
                        if (tokensOperators.Count == 0)
                        {
                            tokensOperators.Add(t);
                        }
                        else
                        {
                            if (tokensOperators[0].TokenType is not (TokenType.OperatorMultiply or TokenType.OperatorDivide))
                            {
                                tokensOperators.Insert(0, t);
                            }
                            else
                            {
                                outputList.Add(tokensOperators[0]);
                                tokensOperators.RemoveAt(0);
                                tokensOperators.Insert(0, t);
                            }
                        }
                    }
                }

                if (tokensOperators.Count > 0)
                {
                    foreach (var t in tokensOperators)
                    {
                        outputList.Add(t);
                    }
                }
            }
            return outputList;
        }

        private static double Evaluation(List<Token> tokens)
        {
            Stack<double> stack = new Stack<double>();
            double tempresult;
            double x;
            double y;
            double result;
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].TokenType == TokenType.Number)
                {
                    stack.Push(tokens[i].Number);
                }
                else
                {
                    if (tokens[i].TokenType == TokenType.OperatorPlus)
                    {
                        x = stack.Pop();
                        y = stack.Pop();
                        tempresult = y + x;
                        stack.Push(tempresult);
                    }
                    if (tokens[i].TokenType == TokenType.OperatorMinus)
                    {
                        x = stack.Pop();
                        y = stack.Pop();
                        tempresult = y - x;
                        stack.Push(tempresult);
                    }
                    if (tokens[i].TokenType == TokenType.OperatorMultiply)
                    {
                        x = stack.Pop();
                        y = stack.Pop();
                        tempresult = y * x;
                        stack.Push(tempresult);
                    }
                    if (tokens[i].TokenType == TokenType.OperatorDivide)
                    {
                        x = stack.Pop();
                        y = stack.Pop();
                        tempresult = y / x;
                        stack.Push(tempresult);
                    }
                }
            }

            result = stack.Pop();
            result = Math.Round(result, 2);
            return result;
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
            var result = parser.ParseExpression_();
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
            var left = ParseNumber();
            var opToken = token[index];
            var right = ParseNumber();
            var operatornNode = new BinaryNode(left, BinaryOperator.Plus, right);
            throw new NotImplementedException();
        }
        private AstNode? ParseMultiplicative()
        {
            /*
            multiplicative
                : number ( Multiplicative_Operator number )*
                ;
            */
            throw new NotImplementedException();
        }

        private AstNode? ParseNumber()
        {
            // number : Number_Literal ; 
            var node = new NumberNode(tokens[index].Number);
            index++;
            return node;
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
            // TODO
            throw new NotImplementedException();
        }

        #endregion Helper Methods
    }
}

