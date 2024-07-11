namespace Assignment
{
    partial class Form1
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
            button1 = new Button();
            pictureBox1 = new PictureBox();
            openFileDialog1 = new OpenFileDialog();
            addButton = new RadioButton();
            deleteButton = new RadioButton();
            selectButton = new RadioButton();
            panel1 = new Panel();
            clearButton = new RadioButton();
            groupBox1 = new GroupBox();
            splitInput = new NumericUpDown();
            splitButton = new Button();
            moveButton = new Button();
            panel3 = new Panel();
            moveY = new NumericUpDown();
            label3 = new Label();
            panel2 = new Panel();
            moveX = new NumericUpDown();
            label1 = new Label();
            button2 = new Button();
            panel5 = new Panel();
            zoomInput = new NumericUpDown();
            label4 = new Label();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            panel6 = new Panel();
            weightInput = new NumericUpDown();
            label2 = new Label();
            increaseCtrlButton = new Button();
            bernsteinButton = new Button();
            deCastButton = new Button();
            panel4 = new Panel();
            tInput = new NumericUpDown();
            label5 = new Label();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitInput).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)moveY).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)moveX).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)zoomInput).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)weightInput).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tInput).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(477, 20);
            button1.Name = "button1";
            button1.Size = new Size(92, 23);
            button1.TabIndex = 1;
            button1.Text = "Browse Files...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(30, 50);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(539, 377);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Object Files|*.obj";
            // 
            // addButton
            // 
            addButton.Appearance = Appearance.Button;
            addButton.Location = new Point(56, 0);
            addButton.Name = "addButton";
            addButton.Size = new Size(50, 25);
            addButton.TabIndex = 3;
            addButton.Text = "Add";
            addButton.TextAlign = ContentAlignment.MiddleCenter;
            addButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            deleteButton.Appearance = Appearance.Button;
            deleteButton.Location = new Point(112, 0);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(50, 25);
            deleteButton.TabIndex = 4;
            deleteButton.Text = "Delete";
            deleteButton.TextAlign = ContentAlignment.MiddleCenter;
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.CheckedChanged += deleteButton_CheckedChanged;
            // 
            // selectButton
            // 
            selectButton.Appearance = Appearance.Button;
            selectButton.Checked = true;
            selectButton.Location = new Point(0, 0);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(50, 25);
            selectButton.TabIndex = 5;
            selectButton.TabStop = true;
            selectButton.Text = "Select";
            selectButton.TextAlign = ContentAlignment.MiddleCenter;
            selectButton.UseVisualStyleBackColor = true;
            selectButton.CheckedChanged += selectButton_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(clearButton);
            panel1.Controls.Add(deleteButton);
            panel1.Controls.Add(selectButton);
            panel1.Controls.Add(addButton);
            panel1.Location = new Point(30, 19);
            panel1.Name = "panel1";
            panel1.Size = new Size(219, 25);
            panel1.TabIndex = 7;
            // 
            // clearButton
            // 
            clearButton.Appearance = Appearance.Button;
            clearButton.Location = new Point(168, 1);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(50, 25);
            clearButton.TabIndex = 6;
            clearButton.Text = "Clear";
            clearButton.TextAlign = ContentAlignment.MiddleCenter;
            clearButton.UseVisualStyleBackColor = true;
            clearButton.CheckedChanged += clearButton_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(splitInput);
            groupBox1.Controls.Add(splitButton);
            groupBox1.Controls.Add(moveButton);
            groupBox1.Controls.Add(panel3);
            groupBox1.Controls.Add(panel2);
            groupBox1.Location = new Point(586, 42);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 197);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Edit";
            // 
            // splitInput
            // 
            splitInput.DecimalPlaces = 3;
            splitInput.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            splitInput.Location = new Point(99, 151);
            splitInput.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            splitInput.Name = "splitInput";
            splitInput.Size = new Size(71, 23);
            splitInput.TabIndex = 11;
            splitInput.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            // 
            // splitButton
            // 
            splitButton.Location = new Point(31, 151);
            splitButton.Name = "splitButton";
            splitButton.Size = new Size(62, 23);
            splitButton.TabIndex = 10;
            splitButton.Text = "Split";
            splitButton.UseVisualStyleBackColor = true;
            splitButton.Click += splitButton_Click;
            // 
            // moveButton
            // 
            moveButton.Location = new Point(50, 104);
            moveButton.Name = "moveButton";
            moveButton.Size = new Size(111, 23);
            moveButton.TabIndex = 9;
            moveButton.Text = "Move Selected";
            moveButton.UseVisualStyleBackColor = true;
            moveButton.Click += moveButton_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(moveY);
            panel3.Controls.Add(label3);
            panel3.Location = new Point(31, 66);
            panel3.Name = "panel3";
            panel3.Size = new Size(143, 29);
            panel3.TabIndex = 9;
            // 
            // moveY
            // 
            moveY.DecimalPlaces = 2;
            moveY.Location = new Point(19, 3);
            moveY.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            moveY.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            moveY.Name = "moveY";
            moveY.Size = new Size(120, 23);
            moveY.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 6);
            label3.Name = "label3";
            label3.Size = new Size(17, 15);
            label3.TabIndex = 2;
            label3.Text = "Y:";
            // 
            // panel2
            // 
            panel2.Controls.Add(moveX);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(31, 34);
            panel2.Name = "panel2";
            panel2.Size = new Size(143, 29);
            panel2.TabIndex = 4;
            // 
            // moveX
            // 
            moveX.DecimalPlaces = 2;
            moveX.Location = new Point(19, 3);
            moveX.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            moveX.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            moveX.Name = "moveX";
            moveX.Size = new Size(120, 23);
            moveX.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 6);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 2;
            label1.Text = "X:";
            // 
            // button2
            // 
            button2.Location = new Point(31, 71);
            button2.Name = "button2";
            button2.Size = new Size(143, 23);
            button2.TabIndex = 10;
            button2.Text = "Focus Object";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // panel5
            // 
            panel5.Controls.Add(zoomInput);
            panel5.Controls.Add(label4);
            panel5.Location = new Point(31, 27);
            panel5.Name = "panel5";
            panel5.Size = new Size(143, 29);
            panel5.TabIndex = 4;
            // 
            // zoomInput
            // 
            zoomInput.Location = new Point(47, 3);
            zoomInput.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            zoomInput.Name = "zoomInput";
            zoomInput.Size = new Size(92, 23);
            zoomInput.TabIndex = 0;
            zoomInput.Value = new decimal(new int[] { 20, 0, 0, 0 });
            zoomInput.ValueChanged += zoomInput_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 6);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 2;
            label4.Text = "Zoom:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(panel5);
            groupBox2.Location = new Point(586, 257);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 117);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "View";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(panel6);
            groupBox3.Controls.Add(increaseCtrlButton);
            groupBox3.Controls.Add(bernsteinButton);
            groupBox3.Controls.Add(deCastButton);
            groupBox3.Controls.Add(panel4);
            groupBox3.Controls.Add(checkBox2);
            groupBox3.Controls.Add(checkBox1);
            groupBox3.Location = new Point(792, 42);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(200, 332);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Bezier";
            // 
            // panel6
            // 
            panel6.Controls.Add(weightInput);
            panel6.Controls.Add(label2);
            panel6.Location = new Point(22, 180);
            panel6.Name = "panel6";
            panel6.Size = new Size(143, 29);
            panel6.TabIndex = 9;
            // 
            // weightInput
            // 
            weightInput.DecimalPlaces = 2;
            weightInput.Enabled = false;
            weightInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            weightInput.Location = new Point(24, 3);
            weightInput.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            weightInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            weightInput.Name = "weightInput";
            weightInput.Size = new Size(116, 23);
            weightInput.TabIndex = 12;
            weightInput.Value = new decimal(new int[] { 1, 0, 0, 0 });
            weightInput.ValueChanged += weightInput_ValueChanged;
            weightInput.EnabledChanged += weightInput_EnabledChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 6);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 2;
            label2.Text = "w:";
            // 
            // increaseCtrlButton
            // 
            increaseCtrlButton.Location = new Point(22, 151);
            increaseCtrlButton.Name = "increaseCtrlButton";
            increaseCtrlButton.Size = new Size(143, 23);
            increaseCtrlButton.TabIndex = 8;
            increaseCtrlButton.Text = "Increase Control Points";
            increaseCtrlButton.UseVisualStyleBackColor = true;
            increaseCtrlButton.Click += increaseCtrlButton_Click;
            // 
            // bernsteinButton
            // 
            bernsteinButton.Location = new Point(101, 22);
            bernsteinButton.Name = "bernsteinButton";
            bernsteinButton.Size = new Size(82, 23);
            bernsteinButton.TabIndex = 7;
            bernsteinButton.Text = "Bernstein";
            bernsteinButton.UseVisualStyleBackColor = true;
            bernsteinButton.Click += bernsteinButton_Click;
            // 
            // deCastButton
            // 
            deCastButton.Location = new Point(21, 22);
            deCastButton.Name = "deCastButton";
            deCastButton.Size = new Size(82, 23);
            deCastButton.TabIndex = 6;
            deCastButton.Text = "De Casteljau";
            deCastButton.UseVisualStyleBackColor = true;
            deCastButton.Click += deCastButton_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(tInput);
            panel4.Controls.Add(label5);
            panel4.Location = new Point(22, 115);
            panel4.Name = "panel4";
            panel4.Size = new Size(143, 29);
            panel4.TabIndex = 5;
            // 
            // tInput
            // 
            tInput.DecimalPlaces = 2;
            tInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            tInput.Location = new Point(24, 3);
            tInput.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            tInput.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            tInput.Name = "tInput";
            tInput.Size = new Size(116, 23);
            tInput.TabIndex = 12;
            tInput.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            tInput.ValueChanged += tInput_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(4, 6);
            label5.Name = "label5";
            label5.Size = new Size(14, 15);
            label5.TabIndex = 2;
            label5.Text = "t:";
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(22, 90);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(134, 19);
            checkBox2.TabIndex = 2;
            checkBox2.Text = "Show Control Points";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += showControlPoints_CheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(22, 65);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(123, 19);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "Show Bezier Curve";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += showBezier_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1001, 450);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Paint += Form1_Paint;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitInput).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)moveY).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)moveX).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)zoomInput).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)weightInput).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tInput).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private PictureBox pictureBox1;
        private OpenFileDialog openFileDialog1;
        private RadioButton addButton;
        private RadioButton deleteButton;
        private RadioButton selectButton;
        private Panel panel1;
        private GroupBox groupBox1;
        private NumericUpDown moveX;
        private Label label1;
        private Panel panel2;
        private Panel panel3;
        private NumericUpDown moveY;
        private Label label3;
        private Button moveButton;
        private Button button2;
        private Panel panel5;
        private NumericUpDown zoomInput;
        private Label label4;
        private GroupBox groupBox2;
        private Button splitButton;
        private NumericUpDown splitInput;
        private GroupBox groupBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Panel panel4;
        private Label label5;
        private NumericUpDown tInput;
        private Button bernsteinButton;
        private Button deCastButton;
        private RadioButton clearButton;
        private Button increaseCtrlButton;
        private Panel panel6;
        private NumericUpDown weightInput;
        private Label label2;
    }
}