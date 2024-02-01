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
        bool accept_digit = true;
        bool accept_operator = false;
        bool accept_decimal = true;
        private ExpressionOptions options { get; set; } = ExpressionOptions.Default;
        public Calculator()
        {

            InitializeComponent();
        }

        private void NumClick_Click(object sender, EventArgs e)
        {
            math.Text += ((Button)sender).Text;
        }

        private void OperatorClick_Click(object sender, EventArgs e)
        {
            math.Text += ((Button)sender).Text;
        }

        private void BackClick_Click(object sender, EventArgs e)
        {
            var text = math.Text.Trim();
            if (text.Length == 0)
            {
                return;
            }
            text = text[..^1];
            math.Text = text;
        }

        private void ClearClick_Click(object sender, EventArgs e)
        {
            math.Text = "";
        }

        private void Equals_Click(object sender, EventArgs e)
        {
            //try
            //{
            
            math.Text += '=' + (engine.Evaluate(math.Text, options).ToString());
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

        private void Settings_Button_Click(object sender, EventArgs e)
        {
            using var settingsForm = new SettingsForm(this.options);
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                this.options = settingsForm.Options;
            }
        }
    }
}