using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography.X509Certificates;
using Calculator;
using Evaluator;

namespace Calculator
{
    public partial class Calculator : Form
    {
        Evaluator.CalculatorEngine engine = new CalculatorEngine();
        KeyboardInput keyboardinput = new KeyboardInput();
        char DecimalPointCharacter = ',';
        char ArgumentSeparator = '.';
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
            ExpressionOptions options = new ExpressionOptions(this.DecimalPointCharacter, this.ArgumentSeparator);
            math.Text += '=' + (engine.Evaluate(math.Text, options)).ToString();
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

        private void DotDec_CheckedChanged_1(object sender, EventArgs e)
        {
            this.DecimalPointCharacter = '.';
            this.ArgumentSeparator = ',';
        }

        private void CommaDec_CheckedChanged_1(object sender, EventArgs e)
        {
            this.DecimalPointCharacter = ',';
            this.ArgumentSeparator = '.';
        }

        private void Info_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Function list:\n" +
                "square root = sqrt(...)\n" +
                "absolute value = abs(...)\n" +
                "sin = sin(...)\n" +
                "cos = cos(...)\n" +
                "tan = tan(...)\n" +
                "clamp = clamp(...)\n" +
                "Just fill ... with an expression."
                );
        }
    }
}