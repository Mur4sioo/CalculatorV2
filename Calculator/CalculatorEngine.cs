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
    internal class CalculatorEngine
    {
        private char[] operators = { '+', '-', '*', '/' };
        private List<string> Tokens = new List<string>();
        private string result;
        public void Process (string math)
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
        }

        public void ShuntingYard()
        {
            List<char> tokensOperators = new List<char>();
            List<string> outputList = new List<string>();
            char[] o1 = { '+', '-' };
            char[] o2 = { '*', '/' };
            bool isnumber;
            while (Tokens.Count >= 0)
            {
                isnumber = double.TryParse(Tokens[i], out double temp);
                if (isnumber)
                {
                    outputList.Add(Tokens[i]);
                }
                else
                {
                    
                }

            }
        }
        public string Evaluation (List <string> values, string result)
        {
            double evalution_result = 0;
            for (int i = 0; i < values.Count - 1; i++) 
            {
                if (double.TryParse(values[i], out double z) == false && values[i] != "=")
                {
                    double x = double.Parse(values[i-1]);
                    double y = double.Parse(values[i+1]);
 
                    switch (values[i])
                    {
                        case "+":
                            evalution_result = x + y;
                            break;
                        case "-":
                            evalution_result = x - y;
                            break;
                        case "*":
                            evalution_result = x * y;
                            break;
                        case "/":
                            evalution_result = x / y;
                            break;
                    }
                    values.RemoveRange(0,3);
                    values.Insert(0, evalution_result.ToString());
                    i = 0;
                }
            }
            values.Clear();
            result = evalution_result.ToString();
            return result;
        }
    }
}
