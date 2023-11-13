namespace Calculator
{
    /*
       - Accept a digit, decimal point, or operator 
       - Accept a digit or an operator 
       - Accept a digit or a decimal point
     */
    public partial class Calculator : Form
    {
        CalculatorEngine engine = new CalculatorEngine();
        KeyboardInput keyboardinput = new KeyboardInput();
        bool accept_digit = true;
        bool accept_operator = false;
        bool accept_decimal = true;
        bool number_is_decimal = false;
        public Calculator()
        {
            InitializeComponent();
        }

        private void NumClick_Click(object sender, EventArgs e)
        {
            if (math.Text == "0")
            {
                math.Text = ((Button)sender).Text;
                accept_decimal = true;
                accept_digit = true;
                accept_operator = true;
            }
            else
            {
                math.Text += ((Button)sender).Text;
                accept_decimal = true;
                accept_digit = true;
                accept_operator = true;
            }
        }

        private void OperatorClick_Click(object sender, EventArgs e)
        {
            while (accept_operator && math.Text != "0")
            {
                math.Text += ((Button)sender).Text;
                accept_digit = true;
                accept_operator = false;
                accept_decimal = false;
                number_is_decimal = false;
            }
        }

        private void BackClick_Click(object sender, EventArgs e)
        {
            string lastchar = math.Text[math.Text.Length - 1].ToString();
            if (math.Text.Length != 1)
            {
                if (accept_operator == false)
                {
                    if (number_is_decimal)
                    {
                        math.Text = math.Text.Remove(math.Text.Length - 1);
                        accept_operator = true;
                        number_is_decimal = true;
                    }
                    else
                    {
                        math.Text = math.Text.Remove(math.Text.Length - 1);
                        accept_operator = true;
                    }
                }
            }
            else
            {
                math.Text = "0";
            }
        }

        private void ClearClick_Click(object sender, EventArgs e)
        {
            math.Text = "0";
            accept_digit = true;
            accept_operator = false;
            accept_decimal = false;
        }

        private void Equals_Click(object sender, EventArgs e)
        {
            math.Text += "=";
            math.Text += engine.Process(math.Text);
        }

        private void decimal_point_Click(object sender, EventArgs e)
        {
            while (accept_decimal && number_is_decimal == false)
            {
                math.Text += ((Button)sender).Text;
                accept_digit = true;
                accept_operator = false;
                accept_decimal = false;
                number_is_decimal = true;
            }
        }
    }
}