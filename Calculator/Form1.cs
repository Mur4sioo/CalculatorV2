using System.Runtime.InteropServices.JavaScript;
using Calculator;
using Evaluator;

namespace Calculator
{
    public partial class Calculator : Form
    {
        Evaluator.CalculatorEngine engine = new CalculatorEngine();
        KeyboardInput keyboardinput = new KeyboardInput();
        bool accept_digit = true;
        bool accept_operator = false;
        bool accept_decimal = true;
        public Calculator()
        {
            InitializeComponent();
        }

        private void NumClick_Click(object sender, EventArgs e)
        {
            if (math.Text == "0")
            {
                math.Text = ((Button)sender).Text;
                accept_digit = true;
                accept_operator = true;
            }
            else
            {
                math.Text += ((Button)sender).Text;
                accept_digit = true;
                accept_operator = true;
            }
        }

        private void OperatorClick_Click(object sender, EventArgs e)
        {
            math.Text += ((Button)sender).Text;
        }

        private void BackClick_Click(object sender, EventArgs e)
        {
            var text = math.Text.Trim();
            if (text.Length == 1)
            {
                math.Text = "0";
                return;
            }
            text = text[..^1];
            math.Text = text;
        }

        private void ClearClick_Click(object sender, EventArgs e)
        {
            math.Text = "0";
            accept_digit = true;
            accept_operator = false;
            accept_decimal = true;
        }

        private void Equals_Click(object sender, EventArgs e)
        {
            math.Text += '=' + (engine.Evaluate(math.Text)).ToString();
        }

        private void decimal_point_Click(object sender, EventArgs e)
        {
            var last_char = math.Text[^1];
            bool lastchar = last_char is not ('+' or '-' or '*' or '/');
            while (accept_decimal)
            {

                if (lastchar == true)
                {
                    math.Text += ((Button)sender).Text;
                    accept_digit = true;
                    accept_operator = false;
                    accept_decimal = false;
                    break;
                }
                else
                {
                    break;
                }
            }
        }
    }
}