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
            groupBox1 = new GroupBox();
            moveButton = new Button();
            panel3 = new Panel();
            moveY = new NumericUpDown();
            label3 = new Label();
            panel2 = new Panel();
            moveX = new NumericUpDown();
            label1 = new Label();
            button2 = new Button();
            panel5 = new Panel();
            label4 = new Label();
            zoomInput = new NumericUpDown();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)moveY).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)moveX).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)zoomInput).BeginInit();
            groupBox2.SuspendLayout();
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
            // 
            // panel1
            // 
            panel1.Controls.Add(deleteButton);
            panel1.Controls.Add(selectButton);
            panel1.Controls.Add(addButton);
            panel1.Location = new Point(30, 19);
            panel1.Name = "panel1";
            panel1.Size = new Size(167, 25);
            panel1.TabIndex = 7;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(moveButton);
            groupBox1.Controls.Add(panel3);
            groupBox1.Controls.Add(panel2);
            groupBox1.Location = new Point(586, 42);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 151);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Edit";
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
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 6);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 2;
            label4.Text = "Zoom:";
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
            // groupBox2
            // 
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(panel5);
            groupBox2.Location = new Point(586, 207);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 117);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "View";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
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
    }
}