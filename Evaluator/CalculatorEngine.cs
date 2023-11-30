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
        private static List<Token> Tokenization(string math)
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
}
