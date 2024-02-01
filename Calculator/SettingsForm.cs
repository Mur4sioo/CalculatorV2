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
    }
}
