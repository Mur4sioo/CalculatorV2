using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Calculator
{
    public class CalculatorEngine
    {
        private char[] operators = { '+', '-', '*', '/' };
        private List<string> Tokens = new List<string>();
        List<string> tokensOperators = new List<string>();
        List<string> outputList = new List<string>();
        public string Tokenization (string math)
        {
            string temp = "";
            foreach (char x in math)
            {
                if (operators.Contains(x))
                {
                    Tokens.Add(temp);
                    temp = "";
                    Tokens.Add(x.ToString());
                }
                else
                {
                    temp += x;
                }    
            }
            Tokens.Add(temp);
            temp = "";
            temp = ShuntingYard();
            Tokens.Clear();
            return temp;
        }

        public string ShuntingYard()
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
        public string Evaluation (List <string> tokenList)
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
