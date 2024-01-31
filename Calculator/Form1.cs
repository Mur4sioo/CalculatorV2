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
            //try
            //{
            math.Text += '=' + (engine.Evaluate(math.Text)).ToString();
            //}
            //catch (Exception ex)
            //{
            //    math.Text = $"Error : {ex}";
            //}
        }

        private void decimal_point_Click(object sender, EventArgs e)
        {
            math.Text += ((Button)sender).Text;
        }

        private void DotDec_CheckedChanged(object sender, EventArgs e)
        {
            ExpressionOptions options = new ExpressionOptions('.', ',');
        }

        private void CommaDec_CheckedChanged(object sender, EventArgs e)
        {
            ExpressionOptions options = new ExpressionOptions(',', '.');
        }
    }
}