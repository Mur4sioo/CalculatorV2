using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using static Calculator.CalculatorEngine;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator
{
    static class CalculatorEngine
    {
        static readonly char[] Operators = { '+', '-', '*', '/' };
        
        public enum TokenType
        {
            OperatorPlus,
            OperatorMinus,
            OperatorDivide,
            OperatorMultiply,
            number,
        }
        public record Token(TokenType TokenType, double Number);

        public static double Evaluate(string math)
        {
            var infixTokens = Tokenization(math);
            var postfixTokens = ShuntingYard(infixTokens);
            var result = Evaluate(postfixTokens);
            return result;
        }
        private static List<Token> Tokenization (string math)
        {
            List<Token> Tokens = new List<Token>();
            var temp = "";
            foreach (char x in math)
            {
                if (Operators.Contains(x))
                {
                    if (temp.Length > 0)
                    {
                        Tokens.Add(new Token(TokenType.number,Convert.ToDouble(temp)));
                    }
                    if (x is '+')
                    {
                        Tokens.Add(new Token(TokenType.OperatorPlus, 0));
                    }
                    if (x is '-')
                    {
                        Tokens.Add(new Token(TokenType.OperatorMinus, 0));
                    }
                    if (x is '*')
                    {
                        Tokens.Add(new Token(TokenType.OperatorMultiply, 0));
                    }
                    if (x is '/')
                    {
                        Tokens.Add(new Token(TokenType.OperatorDivide, 0));
                    }
                }
                else
                {
                    temp += x;
                }
            }
            if (temp.Length > 0)
            {
                Tokens.Add(new Token(TokenType.number, Convert.ToDouble(temp)));
            }

            return Tokens;
        }

        private static List<Token> ShuntingYard(List<Token> Tokens)
        {
            string output = "";
            bool isnumber;
            if (Tokens.Count > 0)
            {
                foreach (var t in Tokens)
                {
                    isnumber = double.TryParse(t, out double temp);
                    if (isnumber)
                    {
                        outputList.Add(t);
                    }
                    else
                    {
                        if (tokensOperators.Count == 0)
                        {
                            tokensOperators.Add(t);
                        }
                        else
                        {
                            if (tokensOperators[0] is not "*" or "/")
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

            tokensOperators.Clear();
            output = Evaluation(outputList);
            outputList.Clear();
            return output;
        }
        private static double Evaluate(List<Token> tokens)
        {
            List<double> stack = new List<double>();
            double tempresult;
            double x;
            double y;
            double result;
            bool isnumber;
            for (int i = 0; i < tokenList.Count; i++) 
            {
                isnumber = double.TryParse(tokenList[i], out double temp);
                if (isnumber)
                {
                    stack.Add(Convert.ToDouble(tokenList[i]));
                }
                else
                {
                    if (tokenList[i] == "+")
                    {
                        x = stack[^2];
                        stack.Remove(x);
                        y = stack[^1];
                        stack.Remove(y);
                        tempresult = x + y;
                        stack.Add(tempresult);
                    }
                    if (tokenList[i] == "-")
                    {
                        x = stack[^2];
                        stack.Remove(x);
                        y = stack[^1];
                        stack.Remove(y);
                        tempresult = x - y;
                        stack.Add(tempresult);
                    }
                    if (tokenList[i] == "*")
                    {
                        x = stack[^2];
                        stack.Remove(x);
                        y = stack[^1];
                        stack.Remove(y);
                        tempresult = x * y;
                        stack.Add(tempresult);
                    }
                    if (tokenList[i] == "/")
                    {
                        x = stack[^2];
                        stack.Remove(x);
                        y = stack[^1];
                        stack.Remove(y);
                        tempresult = x / y;
                        stack.Add(tempresult);
                    }
                }
            }

            result = stack[0];
            result = Math.Round(result,2);
            return result.ToString();
        }
    }
}
