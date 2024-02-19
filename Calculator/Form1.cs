using System.Globalization;
using System.Runtime.CompilerServices;
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
        private Dictionary<string, double> variables = new Dictionary<string, double>();
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

                var doubleResult = engine.Evaluate(math.Text, variables, options);
                var textResult = doubleResult.ToString(CultureInfo.InvariantCulture);
                var convertedTextResult = ExpressionOptions.ChangeDecimalPoint(
                    textResult,
                    convertFrom: '.',
                    convertTo: options.DecimalPointCharacter
                );
                ResultHistory.Text = $"{math.Text}={convertedTextResult}";
                math.Text = "";
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Invalid input.");
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

        private void variablesButton_Click(object sender, EventArgs e)
        {
            using var variablesForm = new VariablesForm(variables);
            if (variablesForm.ShowDialog() == DialogResult.OK)
            {
                this.variables = variablesForm.Variables;

            }
        }
    }
}