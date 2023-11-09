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
        char[] operators = { '+', '-', '*', '/', '.', '=' };
        List<string> math_process = new List<string>();
        string result;
        string temp = "";
        public bool IsValid (string math)
        {
            char last = math[math.Length - 1];
            if (operators.Any(x => math.EndsWith(x)))
            {
                return true;
            }
            return false;
        }
        public string Process (string math)
        {
            string temp = "";
            foreach (char x in math)
            {
                if (operators.Contains(x))
                {
                    math_process.Add(temp);
                    temp = "";
                    math_process.Add(x.ToString());
                }
                else
                {
                    temp += x;
                }    
            }
            math = Evaluation(math_process, result);
            return math.ToString();
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
