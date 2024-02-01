namespace Calculator
{
    partial class SettingsForm
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
            DotDec = new RadioButton();
            CommaDec = new RadioButton();
            CommaDecimalText = new Label();
            DotDecText = new Label();
            label1 = new Label();
            Info_button = new Button();
            okButton = new Button();
            cancelButton = new Button();
            Settings_Label = new Label();
            SuspendLayout();
            // 
            // DotDec
            // 
            DotDec.AutoSize = true;
            DotDec.Location = new Point(350, 139);
            DotDec.Name = "DotDec";
            DotDec.Size = new Size(14, 13);
            DotDec.TabIndex = 31;
            DotDec.UseVisualStyleBackColor = true;
            DotDec.CheckedChanged += DotDec_CheckedChanged;
            // 
            // CommaDec
            // 
            CommaDec.AutoSize = true;
            CommaDec.Checked = true;
            CommaDec.Location = new Point(205, 139);
            CommaDec.Name = "CommaDec";
            CommaDec.Size = new Size(14, 13);
            CommaDec.TabIndex = 30;
            CommaDec.TabStop = true;
            CommaDec.UseVisualStyleBackColor = true;
            CommaDec.CheckedChanged += CommaDec_CheckedChanged;
            // 
            // CommaDecimalText
            // 
            CommaDecimalText.Location = new Point(150, 88);
            CommaDecimalText.Name = "CommaDecimalText";
            CommaDecimalText.Size = new Size(139, 48);
            CommaDecimalText.TabIndex = 29;
            CommaDecimalText.Text = "DecimalPoint = \",\" ArgumentSeparator = \".\"";
            // 
            // DotDecText
            // 
            DotDecText.Location = new Point(295, 88);
            DotDecText.Name = "DotDecText";
            DotDecText.Size = new Size(139, 48);
            DotDecText.TabIndex = 28;
            DotDecText.Text = "DecimalPoint = \".\" ArgumentSeparator = \",\"";
            // 
            // label1
            // 
            label1.Location = new Point(12, 88);
            label1.Name = "label1";
            label1.Size = new Size(132, 48);
            label1.TabIndex = 32;
            label1.Text = "Decimal Point and Argument Separator :";
            // 
            // Info_button
            // 
            Info_button.Location = new Point(12, 185);
            Info_button.Name = "Info_button";
            Info_button.Size = new Size(139, 66);
            Info_button.TabIndex = 33;
            Info_button.Text = "Info";
            Info_button.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(157, 185);
            okButton.Name = "okButton";
            okButton.Size = new Size(132, 66);
            okButton.TabIndex = 34;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(295, 185);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(139, 66);
            cancelButton.TabIndex = 35;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // Settings_Label
            // 
            Settings_Label.AutoSize = true;
            Settings_Label.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            Settings_Label.Location = new Point(157, 9);
            Settings_Label.Name = "Settings_Label";
            Settings_Label.Size = new Size(141, 46);
            Settings_Label.TabIndex = 36;
            Settings_Label.Text = "Settings";
            // 
            // SettingsForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(446, 281);
            Controls.Add(Settings_Label);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(Info_button);
            Controls.Add(label1);
            Controls.Add(DotDec);
            Controls.Add(CommaDec);
            Controls.Add(CommaDecimalText);
            Controls.Add(DotDecText);
            Name = "SettingsForm";
            Text = "Settings_Form";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton DotDec;
        private RadioButton CommaDec;
        private Label CommaDecimalText;
        private Label DotDecText;
        private Label label1;
        private Button Info_button;
        private Button okButton;
        private Button cancelButton;
        private Label Settings_Label;
    }
}