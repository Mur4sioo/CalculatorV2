namespace Calculator
{
    public partial class Calculator : Form
    {
        CalculatorEngine engine = new CalculatorEngine();
        KeyboardInput keyboardinput = new KeyboardInput();
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
            while (engine.IsValid(math.Text) == false)
            {
                math.Text += ((Button)sender).Text;
            }
        }

        private void BackClick_Click(object sender, EventArgs e)
        {
            math.Text = math.Text.Remove(math.Text.Length - 1);
        }

        private void ClearClick_Click(object sender, EventArgs e)
        {
            math.Text = "";
        }

        private void Equals_Click(object sender, EventArgs e)
        {
            math.Text += "=";
            math.Text += engine.Process(math.Text);
        }
    }
}