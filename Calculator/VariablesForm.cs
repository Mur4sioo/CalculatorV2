using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class VariablesForm : Form
    {
        public Dictionary<string, double> Variables = new Dictionary<string, double>();
        public VariablesForm(Dictionary<string, double> variables) 
        {
            this.Variables = variables;
            InitializeComponent();
            foreach (var variable in variables)
            {
                variablesList.Items.Add($"{variable.Key} = {variable.Value}");
            }
        }

        private void variablesList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void VariableNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void VariableValueTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddVariiable_Click(object sender, EventArgs e)
        {
            if (VariableNameTextBox.Text.Length == 1 && double.TryParse(VariableValueTextBox.Text, out double variableValue) && !Variables.ContainsKey(VariableNameTextBox.Text))
            {
                Variables.Add(VariableNameTextBox.Text, variableValue);
                variablesList.Items.Add($"{VariableNameTextBox.Text} = {variableValue}");
                VariableNameTextBox.Clear();
                VariableValueTextBox.Clear();
            }
            else
                MessageBox.Show($"Invalid variable format or variable name is already in use.");
        }

        private void ClearVariables_Click(object sender, EventArgs e)
        {
            variablesList.Clear();
        }
    }
}
