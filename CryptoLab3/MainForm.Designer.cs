﻿
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
            this.sequencePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sequencePanel
            // 
            this.sequencePanel.BackColor = System.Drawing.Color.Gainsboro;
            this.sequencePanel.Controls.Add(this.testWrongPQButton);
            this.sequencePanel.Controls.Add(this.testCryptogramButton);
            this.sequencePanel.Controls.Add(this.testsOutputLabel);
            this.sequencePanel.Controls.Add(this.decryptButton);
            this.sequencePanel.Controls.Add(this.testsOutputRichTextBox);
            this.sequencePanel.Controls.Add(this.encryptButton);
            this.sequencePanel.Location = new System.Drawing.Point(10, 9);
            this.sequencePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sequencePanel.Name = "sequencePanel";
            this.sequencePanel.Size = new System.Drawing.Size(331, 271);
            this.sequencePanel.TabIndex = 1;
            // 
            // testWrongPQButton
            // 
            this.testWrongPQButton.Location = new System.Drawing.Point(167, 247);
            this.testWrongPQButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.testWrongPQButton.Name = "testWrongPQButton";
            this.testWrongPQButton.Size = new System.Drawing.Size(161, 22);
            this.testWrongPQButton.TabIndex = 5;
            this.testWrongPQButton.Text = "Test wrong P and Q";
            this.testWrongPQButton.UseVisualStyleBackColor = true;
            this.testWrongPQButton.Click += new System.EventHandler(this.testWrongPQButton_Click);
            // 
            // testCryptogramButton
            // 
            this.testCryptogramButton.Location = new System.Drawing.Point(3, 247);
            this.testCryptogramButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.testsOutputLabel.Location = new System.Drawing.Point(3, 2);
            this.testsOutputLabel.Name = "testsOutputLabel";
            this.testsOutputLabel.Size = new System.Drawing.Size(73, 15);
            this.testsOutputLabel.TabIndex = 3;
            this.testsOutputLabel.Text = "Tests Output";
            // 
            // decryptButton
            // 
            this.decryptButton.Location = new System.Drawing.Point(167, 221);
            this.decryptButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(161, 22);
            this.decryptButton.TabIndex = 4;
            this.decryptButton.Text = "Decrypt";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // testsOutputRichTextBox
            // 
            this.testsOutputRichTextBox.Location = new System.Drawing.Point(2, 19);
            this.testsOutputRichTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.testsOutputRichTextBox.Name = "testsOutputRichTextBox";
            this.testsOutputRichTextBox.Size = new System.Drawing.Size(326, 198);
            this.testsOutputRichTextBox.TabIndex = 0;
            this.testsOutputRichTextBox.Text = "";
            // 
            // encryptButton
            // 
            this.encryptButton.Location = new System.Drawing.Point(3, 221);
            this.encryptButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.chartComboBox.Location = new System.Drawing.Point(3, 245);
            this.chartComboBox.Name = "chartComboBox";
            this.chartComboBox.Size = new System.Drawing.Size(282, 23);
            this.chartComboBox.TabIndex = 6;
            // 
            // plotButton
            // 
            this.plotButton.Location = new System.Drawing.Point(291, 246);
            this.plotButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.settingsLabel.Location = new System.Drawing.Point(3, 224);
            this.settingsLabel.Name = "settingsLabel";
            this.settingsLabel.Size = new System.Drawing.Size(118, 15);
            this.settingsLabel.TabIndex = 2;
            this.settingsLabel.Text = "Select a chart to plot:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.chartComboBox);
            this.panel1.Controls.Add(this.settingsLabel);
            this.panel1.Controls.Add(this.plotButton);
            this.panel1.Location = new System.Drawing.Point(354, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 271);
            this.panel1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(695, 289);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sequencePanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
    }
}

