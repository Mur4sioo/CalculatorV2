using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Evaluator;

namespace Calculator
{
    public partial class SettingsForm : Form
    {
        private char decimalPointCharacter;
        private char argumentSeparator;

        public ExpressionOptions Options => new ExpressionOptions(decimalPointCharacter: this.decimalPointCharacter,
            argumentSeparator: this.argumentSeparator);
        public SettingsForm(ExpressionOptions options)
        {
            InitializeComponent();
            decimalPointCharacter = options.DecimalPointCharacter;
            argumentSeparator = options.ArgumentSeparator;
            if (decimalPointCharacter == '.')
                DotDec.Checked = true;
            else
                CommaDec.Checked = true;
        }

        private void CommaDec_CheckedChanged(object sender, EventArgs e)
        {
            decimalPointCharacter = ',';
            argumentSeparator = '.';
        }

        private void DotDec_CheckedChanged(object sender, EventArgs e)
        {
            decimalPointCharacter = '.';
            argumentSeparator = ',';
        }

        private void Info_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Additional Fuctions:\n" +
                            "2**3 => 2 to the power of 3\n" +
                            "sqrt(2) => square of 2\n" +
                            "abs(2) => absolute value of 2\n" +
                            "sin(3) => sine of 3\n" +
                            "cos(3) => cosine of 3\n" +
                            "tan(3) => tangens of 3\n" +
                            "clamp(4,2,5) => clamp function");
        }
    }
}
