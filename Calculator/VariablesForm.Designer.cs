namespace Calculator
{
    partial class VariablesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            variablesList = new ListView();
            AddVariiable = new Button();
            ClearVariables = new Button();
            VariableNameTextBox = new TextBox();
            VariableValueTextBox = new TextBox();
            OkButton = new Button();
            CancelButton = new Button();
            VariableNameLabel = new Label();
            VariableValueLabel = new Label();
            SuspendLayout();
            // 
            // variablesList
            // 
            variablesList.Location = new Point(12, 12);
            variablesList.Name = "variablesList";
            variablesList.Size = new Size(187, 134);
            variablesList.TabIndex = 0;
            variablesList.UseCompatibleStateImageBehavior = false;
            variablesList.View = View.Details;
            variablesList.SelectedIndexChanged += variablesList_SelectedIndexChanged;
            // 
            // AddVariiable
            // 
            AddVariiable.Location = new Point(12, 203);
            AddVariiable.Name = "AddVariiable";
            AddVariiable.Size = new Size(89, 52);
            AddVariiable.TabIndex = 1;
            AddVariiable.Text = "Add";
            AddVariiable.UseVisualStyleBackColor = true;
            AddVariiable.Click += AddVariiable_Click;
            // 
            // ClearVariables
            // 
            ClearVariables.Location = new Point(107, 203);
            ClearVariables.Name = "ClearVariables";
            ClearVariables.Size = new Size(92, 52);
            ClearVariables.TabIndex = 2;
            ClearVariables.Text = "Clear";
            ClearVariables.UseVisualStyleBackColor = true;
            ClearVariables.Click += ClearVariables_Click;
            // 
            // VariableNameTextBox
            // 
            VariableNameTextBox.Location = new Point(12, 174);
            VariableNameTextBox.Name = "VariableNameTextBox";
            VariableNameTextBox.Size = new Size(89, 23);
            VariableNameTextBox.TabIndex = 3;
            VariableNameTextBox.TextChanged += VariableNameTextBox_TextChanged;
            // 
            // VariableValueTextBox
            // 
            VariableValueTextBox.Location = new Point(107, 174);
            VariableValueTextBox.Name = "VariableValueTextBox";
            VariableValueTextBox.Size = new Size(92, 23);
            VariableValueTextBox.TabIndex = 4;
            VariableValueTextBox.TextChanged += VariableValueTextBox_TextChanged;
            // 
            // OkButton
            // 
            OkButton.DialogResult = DialogResult.OK;
            OkButton.Location = new Point(12, 261);
            OkButton.Name = "OkButton";
            OkButton.Size = new Size(89, 48);
            OkButton.TabIndex = 5;
            OkButton.Text = "Ok";
            OkButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            CancelButton.DialogResult = DialogResult.Cancel;
            CancelButton.Location = new Point(107, 261);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(92, 48);
            CancelButton.TabIndex = 6;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            // 
            // VariableNameLabel
            // 
            VariableNameLabel.AutoSize = true;
            VariableNameLabel.Location = new Point(12, 156);
            VariableNameLabel.Name = "VariableNameLabel";
            VariableNameLabel.Size = new Size(84, 15);
            VariableNameLabel.TabIndex = 7;
            VariableNameLabel.Text = "Variable name:";
            // 
            // VariableValueLabel
            // 
            VariableValueLabel.AutoSize = true;
            VariableValueLabel.Location = new Point(107, 156);
            VariableValueLabel.Name = "VariableValueLabel";
            VariableValueLabel.Size = new Size(82, 15);
            VariableValueLabel.TabIndex = 8;
            VariableValueLabel.Text = "Variable value:";
            // 
            // VariablesForm
            // 
            AcceptButton = OkButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(217, 318);
            Controls.Add(VariableValueLabel);
            Controls.Add(VariableNameLabel);
            Controls.Add(CancelButton);
            Controls.Add(OkButton);
            Controls.Add(VariableValueTextBox);
            Controls.Add(VariableNameTextBox);
            Controls.Add(ClearVariables);
            Controls.Add(AddVariiable);
            Controls.Add(variablesList);
            Name = "VariablesForm";
            Text = "VariablesForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView variablesList;
        private Button AddVariiable;
        private Button ClearVariables;
        private TextBox VariableNameTextBox;
        private TextBox VariableValueTextBox;
        private Button OkButton;
        private Button CancelButton;
        private Label VariableNameLabel;
        private Label VariableValueLabel;
    }
}