namespace Calculator
{
    partial class Calculator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            NumClick = new Button();
            math = new TextBox();
            BackClick = new Button();
            Equals = new Button();
            ClearClick = new Button();
            OperatorClick = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            button10 = new Button();
            button11 = new Button();
            button12 = new Button();
            button13 = new Button();
            SuspendLayout();
            // 
            // NumClick
            // 
            NumClick.Location = new Point(27, 332);
            NumClick.Name = "NumClick";
            NumClick.Size = new Size(80, 80);
            NumClick.TabIndex = 0;
            NumClick.Text = "1";
            NumClick.UseVisualStyleBackColor = true;
            NumClick.Click += NumClick_Click;
            // 
            // math
            // 
            math.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            math.Location = new Point(27, 26);
            math.Multiline = true;
            math.Name = "math";
            math.Size = new Size(424, 128);
            math.TabIndex = 1;
            // 
            // BackClick
            // 
            BackClick.Location = new Point(371, 160);
            BackClick.Name = "BackClick";
            BackClick.Size = new Size(80, 80);
            BackClick.TabIndex = 2;
            BackClick.Text = "<-";
            BackClick.UseVisualStyleBackColor = true;
            BackClick.Click += BackClick_Click;
            // 
            // Equals
            // 
            Equals.Location = new Point(199, 418);
            Equals.Name = "Equals";
            Equals.Size = new Size(80, 80);
            Equals.TabIndex = 3;
            Equals.Text = "=";
            Equals.UseVisualStyleBackColor = true;
            Equals.Click += Equals_Click;
            // 
            // ClearClick
            // 
            ClearClick.Location = new Point(371, 246);
            ClearClick.Name = "ClearClick";
            ClearClick.Size = new Size(80, 80);
            ClearClick.TabIndex = 4;
            ClearClick.Text = "C";
            ClearClick.UseVisualStyleBackColor = true;
            ClearClick.Click += ClearClick_Click;
            // 
            // OperatorClick
            // 
            OperatorClick.Location = new Point(285, 418);
            OperatorClick.Name = "OperatorClick";
            OperatorClick.Size = new Size(80, 80);
            OperatorClick.TabIndex = 5;
            OperatorClick.Text = "/";
            OperatorClick.UseVisualStyleBackColor = true;
            OperatorClick.Click += OperatorClick_Click;
            // 
            // button1
            // 
            button1.Location = new Point(27, 418);
            button1.Name = "button1";
            button1.Size = new Size(80, 80);
            button1.TabIndex = 6;
            button1.Text = ".";
            button1.UseVisualStyleBackColor = true;
            button1.Click += NumClick_Click;
            // 
            // button2
            // 
            button2.Location = new Point(113, 418);
            button2.Name = "button2";
            button2.Size = new Size(80, 80);
            button2.TabIndex = 7;
            button2.Text = "0";
            button2.UseVisualStyleBackColor = true;
            button2.Click += NumClick_Click;
            // 
            // button3
            // 
            button3.Location = new Point(113, 332);
            button3.Name = "button3";
            button3.Size = new Size(80, 80);
            button3.TabIndex = 8;
            button3.Text = "2";
            button3.UseVisualStyleBackColor = true;
            button3.Click += NumClick_Click;
            // 
            // button4
            // 
            button4.Location = new Point(27, 246);
            button4.Name = "button4";
            button4.Size = new Size(80, 80);
            button4.TabIndex = 9;
            button4.Text = "4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += NumClick_Click;
            // 
            // button5
            // 
            button5.Location = new Point(113, 246);
            button5.Name = "button5";
            button5.Size = new Size(80, 80);
            button5.TabIndex = 10;
            button5.Text = "5";
            button5.UseVisualStyleBackColor = true;
            button5.Click += NumClick_Click;
            // 
            // button6
            // 
            button6.Location = new Point(199, 246);
            button6.Name = "button6";
            button6.Size = new Size(80, 80);
            button6.TabIndex = 11;
            button6.Text = "6";
            button6.UseVisualStyleBackColor = true;
            button6.Click += NumClick_Click;
            // 
            // button7
            // 
            button7.Location = new Point(199, 160);
            button7.Name = "button7";
            button7.Size = new Size(80, 80);
            button7.TabIndex = 12;
            button7.Text = "9";
            button7.UseVisualStyleBackColor = true;
            button7.Click += NumClick_Click;
            // 
            // button8
            // 
            button8.Location = new Point(113, 160);
            button8.Name = "button8";
            button8.Size = new Size(80, 80);
            button8.TabIndex = 13;
            button8.Text = "8";
            button8.UseVisualStyleBackColor = true;
            button8.Click += NumClick_Click;
            // 
            // button9
            // 
            button9.Location = new Point(27, 160);
            button9.Name = "button9";
            button9.Size = new Size(80, 80);
            button9.TabIndex = 14;
            button9.Text = "7";
            button9.UseVisualStyleBackColor = true;
            button9.Click += NumClick_Click;
            // 
            // button10
            // 
            button10.Location = new Point(199, 332);
            button10.Name = "button10";
            button10.Size = new Size(80, 80);
            button10.TabIndex = 15;
            button10.Text = "3";
            button10.UseVisualStyleBackColor = true;
            button10.Click += NumClick_Click;
            // 
            // button11
            // 
            button11.Location = new Point(285, 332);
            button11.Name = "button11";
            button11.Size = new Size(80, 80);
            button11.TabIndex = 16;
            button11.Text = "*";
            button11.UseVisualStyleBackColor = true;
            button11.Click += OperatorClick_Click;
            // 
            // button12
            // 
            button12.Location = new Point(285, 246);
            button12.Name = "button12";
            button12.Size = new Size(80, 80);
            button12.TabIndex = 17;
            button12.Text = "-";
            button12.UseVisualStyleBackColor = true;
            button12.Click += OperatorClick_Click;
            // 
            // button13
            // 
            button13.Location = new Point(285, 160);
            button13.Name = "button13";
            button13.Size = new Size(80, 80);
            button13.TabIndex = 18;
            button13.Text = "+";
            button13.UseVisualStyleBackColor = true;
            button13.Click += OperatorClick_Click;
            // 
            // Calculator
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(492, 546);
            Controls.Add(button13);
            Controls.Add(button12);
            Controls.Add(button11);
            Controls.Add(button10);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(OperatorClick);
            Controls.Add(ClearClick);
            Controls.Add(Equals);
            Controls.Add(BackClick);
            Controls.Add(math);
            Controls.Add(NumClick);
            Name = "Calculator";
            Text = "Calculator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button NumClick;
        private TextBox math;
        private Button BackClick;
        private Button Equals;
        private Button ClearClick;
        private Button OperatorClick;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button button10;
        private Button button11;
        private Button button12;
        private Button button13;
    }
}