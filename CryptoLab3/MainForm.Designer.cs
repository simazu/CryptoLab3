
namespace CryptoLab3
{
    partial class MainForm
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
            this.sequencePanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.testWrongPQButton = new System.Windows.Forms.Button();
            this.testCryptogramButton = new System.Windows.Forms.Button();
            this.testsOutputLabel = new System.Windows.Forms.Label();
            this.decryptButton = new System.Windows.Forms.Button();
            this.testsOutputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.encryptButton = new System.Windows.Forms.Button();
            this.chartComboBox = new System.Windows.Forms.ComboBox();
            this.plotButton = new System.Windows.Forms.Button();
            this.settingsLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.sequencePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sequencePanel
            // 
            this.sequencePanel.BackColor = System.Drawing.Color.Gainsboro;
            this.sequencePanel.Controls.Add(this.button1);
            this.sequencePanel.Controls.Add(this.testWrongPQButton);
            this.sequencePanel.Controls.Add(this.testCryptogramButton);
            this.sequencePanel.Controls.Add(this.testsOutputLabel);
            this.sequencePanel.Controls.Add(this.decryptButton);
            this.sequencePanel.Controls.Add(this.testsOutputRichTextBox);
            this.sequencePanel.Controls.Add(this.encryptButton);
            this.sequencePanel.Location = new System.Drawing.Point(10, 9);
            this.sequencePanel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.sequencePanel.Name = "sequencePanel";
            this.sequencePanel.Size = new System.Drawing.Size(331, 599);
            this.sequencePanel.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 567);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Differnt length keys";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.GetKeysButton_Click);
            // 
            // testWrongPQButton
            // 
            this.testWrongPQButton.Location = new System.Drawing.Point(167, 540);
            this.testWrongPQButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.testWrongPQButton.Name = "testWrongPQButton";
            this.testWrongPQButton.Size = new System.Drawing.Size(161, 22);
            this.testWrongPQButton.TabIndex = 5;
            this.testWrongPQButton.Text = "Test wrong P and Q";
            this.testWrongPQButton.UseVisualStyleBackColor = true;
            this.testWrongPQButton.Click += new System.EventHandler(this.testWrongPQButton_Click);
            // 
            // testCryptogramButton
            // 
            this.testCryptogramButton.Location = new System.Drawing.Point(4, 540);
            this.testCryptogramButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.testCryptogramButton.Name = "testCryptogramButton";
            this.testCryptogramButton.Size = new System.Drawing.Size(161, 22);
            this.testCryptogramButton.TabIndex = 2;
            this.testCryptogramButton.Text = "Test cryptogram";
            this.testCryptogramButton.UseVisualStyleBackColor = true;
            this.testCryptogramButton.Click += new System.EventHandler(this.testsButton_Click);
            // 
            // testsOutputLabel
            // 
            this.testsOutputLabel.AutoSize = true;
            this.testsOutputLabel.Location = new System.Drawing.Point(4, 2);
            this.testsOutputLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.testsOutputLabel.Name = "testsOutputLabel";
            this.testsOutputLabel.Size = new System.Drawing.Size(45, 15);
            this.testsOutputLabel.TabIndex = 3;
            this.testsOutputLabel.Text = "Output";
            // 
            // decryptButton
            // 
            this.decryptButton.Location = new System.Drawing.Point(167, 515);
            this.decryptButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(161, 22);
            this.decryptButton.TabIndex = 4;
            this.decryptButton.Text = "Decrypt";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // testsOutputRichTextBox
            // 
            this.testsOutputRichTextBox.Location = new System.Drawing.Point(2, 18);
            this.testsOutputRichTextBox.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.testsOutputRichTextBox.Name = "testsOutputRichTextBox";
            this.testsOutputRichTextBox.Size = new System.Drawing.Size(326, 493);
            this.testsOutputRichTextBox.TabIndex = 0;
            this.testsOutputRichTextBox.Text = "";
            // 
            // encryptButton
            // 
            this.encryptButton.Location = new System.Drawing.Point(4, 515);
            this.encryptButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(161, 22);
            this.encryptButton.TabIndex = 2;
            this.encryptButton.Text = "Encrypt";
            this.encryptButton.UseVisualStyleBackColor = true;
            this.encryptButton.Click += new System.EventHandler(this.encryptButton_Click);
            // 
            // chartComboBox
            // 
            this.chartComboBox.FormattingEnabled = true;
            this.chartComboBox.Items.AddRange(new object[] {
            "Attack to RSA",
            "Factorization time from difference beetwen P and Q",
            "Encryption time from text bit length",
            "Decryption time from text bit length",
            "Ciphertext growth factor from from text bit length"});
            this.chartComboBox.Location = new System.Drawing.Point(4, 569);
            this.chartComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chartComboBox.Name = "chartComboBox";
            this.chartComboBox.Size = new System.Drawing.Size(282, 23);
            this.chartComboBox.TabIndex = 6;
            // 
            // plotButton
            // 
            this.plotButton.Location = new System.Drawing.Point(290, 570);
            this.plotButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.plotButton.Name = "plotButton";
            this.plotButton.Size = new System.Drawing.Size(38, 22);
            this.plotButton.TabIndex = 5;
            this.plotButton.Text = "Plot";
            this.plotButton.UseVisualStyleBackColor = true;
            this.plotButton.Click += new System.EventHandler(this.plotButton_Click);
            // 
            // settingsLabel
            // 
            this.settingsLabel.AutoSize = true;
            this.settingsLabel.Location = new System.Drawing.Point(4, 548);
            this.settingsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.settingsLabel.Name = "settingsLabel";
            this.settingsLabel.Size = new System.Drawing.Size(118, 15);
            this.settingsLabel.TabIndex = 2;
            this.settingsLabel.Text = "Select a chart to plot:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.StatusLabel);
            this.panel1.Controls.Add(this.chartComboBox);
            this.panel1.Controls.Add(this.settingsLabel);
            this.panel1.Controls.Add(this.plotButton);
            this.panel1.Location = new System.Drawing.Point(354, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 598);
            this.panel1.TabIndex = 2;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(335, 574);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 15);
            this.StatusLabel.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1260, 611);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sequencePanel);
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "MainForm";
            this.Text = "RSA";
            this.sequencePanel.ResumeLayout(false);
            this.sequencePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel sequencePanel;
        private System.Windows.Forms.Label settingsLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button testCryptogramButton;
        private System.Windows.Forms.RichTextBox testsOutputRichTextBox;
        private System.Windows.Forms.Button decryptButton;
        private System.Windows.Forms.Button encryptButton;
        private System.Windows.Forms.ComboBox chartComboBox;
        private System.Windows.Forms.Button plotButton;
        private System.Windows.Forms.Label testsOutputLabel;
        private System.Windows.Forms.Button testWrongPQButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label StatusLabel;
    }
}

