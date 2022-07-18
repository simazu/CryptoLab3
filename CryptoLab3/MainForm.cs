using CryptoLab3.Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Windows.Forms.DataVisualization.Charting;

namespace CryptoLab3
{
    public partial class MainForm : Form
    {
        private Chart chart1;

        const string PublicKeyFilePath = "publicKey.txt";
        const string PrivateKeyFilePath = "privateKey.txt";
        public MainForm()
        {
            InitializeComponent();
            InitChart();
        }

        private void testsButton_Click(object sender, EventArgs e)
        {
            testsOutputRichTextBox.Clear();
            foreach (int openExponent in new[] { 3, 65537 })
            {
                testsOutputRichTextBox.Text += $"\n____________________________\nencrypted text test, e = {openExponent}\n____________________________\n\n";
                MyRSA rsa = new MyRSA(128, openExponent);
                rsa.Encrypt("text.txt");

                string encryptedText = Converter.FileToBits("encrypted.txt");
                Test(encryptedText);
            }
        }

        private void InitChart()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();

            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.Controls.Add(this.chart1);


            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(850, 500);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";

            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
        }

        private void Test(string sequence)
        {
            for (int i = 2; i < 5; i++)
            {
                (double, double, double) serialTestResult = MSequenceTester.SerialTest(sequence, i);
                testsOutputRichTextBox.Text += $"Serial test, serial length = {i}\n{serialTestResult.Item3} - AlphaMax: {serialTestResult.Item1}, AlphaMin: {serialTestResult.Item2}" +
                    $", test {((serialTestResult.Item3 <= serialTestResult.Item1 && serialTestResult.Item1 >= serialTestResult.Item2) ? "passed" : "failed")}\n";
            }
            testsOutputRichTextBox.Text += "\n";

            (double, double, double) pokerTestResult = MSequenceTester.PokerTest(sequence);
            testsOutputRichTextBox.Text += $"Poker test, {pokerTestResult.Item3} - AlphaMax: {pokerTestResult.Item1}, AlphaMin: {pokerTestResult.Item2}" +
                $", test {((pokerTestResult.Item3 <= pokerTestResult.Item1 && pokerTestResult.Item1 >= pokerTestResult.Item2) ? "passed" : "failed")}\n\n";

            int[] k = { 1, 2, 8, 9 };
            foreach (int i in k)
            {
                (double, double) correlationTestResult = MSequenceTester.CorrelationTest(sequence, i);
                testsOutputRichTextBox.Text += $"Correlation Test, k = {i}\nR = {correlationTestResult.Item1}, Rref = {correlationTestResult.Item2}" +
                    $", test {(correlationTestResult.Item1 < correlationTestResult.Item2 ? "passed" : "failed")}\n";
            }
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            string fileName = "";
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                    fileName = fileDialog.FileName;
                else
                    return;
            }

            MyRSA rsa = new MyRSA(512, 17);
            File.WriteAllText(PublicKeyFilePath, rsa.GetPublicKey().Item1.ToString() + "\n" + rsa.GetPublicKey().Item2.ToString());
            File.WriteAllText(PrivateKeyFilePath, rsa.GetPrivateKey().Item1.ToString() + "\n" + rsa.GetPrivateKey().Item2.ToString());

            rsa.Encrypt(fileName);
            testsOutputRichTextBox.Clear();
            testsOutputRichTextBox.Text += $"encrypted to {Directory.GetCurrentDirectory()}\\encrypted.txt ";
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            string fileName = "encrypted.txt";
            StreamReader sr = new StreamReader(PrivateKeyFilePath);
            (BigInteger, BigInteger) privateKey = (BigInteger.Parse(sr.ReadLine()), BigInteger.Parse(sr.ReadLine()));
            sr.Close();
            MyRSA.Decrypt(fileName, privateKey);
            testsOutputRichTextBox.Clear();
            testsOutputRichTextBox.Text += $"decrypted to {Directory.GetCurrentDirectory()}\\decrypted.txt ";
        }

        private void testWrongPQButton_Click(object sender, EventArgs e)
        {
            testsOutputRichTextBox.Clear();
            string fileName = "";
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                    fileName = fileDialog.FileName;
                else
                    return;
            }

            BigInteger P = Algorithms.GetSimpleRandomNumber(128) + 1;
            BigInteger Q = Algorithms.GetSimpleRandomNumber(128) + 1;
            MyRSA rsa = new MyRSA(P, Q);
            testsOutputRichTextBox.Text += $"P\n{P}\nQ\n{Q}\n";
            rsa.Encrypt(fileName);
            testsOutputRichTextBox.Text += "encrypted to encrypted.txt\n";
            rsa.Decrypt("encrypted.txt");
            testsOutputRichTextBox.Text += "decrypted to decrypted.txt\n";
        }

        private async void plotButton_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            switch (chartComboBox.SelectedIndex)
            {
                case 0:
                    {
                        StatusLabel.Text = "Calculation in progress...";
                        (int[] x0, TimeSpan[] y0) = await Task.Run(() => RSATester.RSAAttack());

                        Series series = chart1.Series.Add("Attack");
                        series.ChartType = SeriesChartType.Line;

                        for (int i = 0; i < x0.Length; i++)
                        {
                            series.Points.AddXY(x0[i], y0[i].TotalMilliseconds);
                        }
                        StatusLabel.Text = "";
                        break;
                    }

                case 1:
                    {
                        StatusLabel.Text = "Calculation in progress...";
                        (double[] x1, TimeSpan[] y1) = await Task.Run(() => RSATester.FactorizationTimeFromPQDifference());

                        Series series = chart1.Series.Add("Factorization Time");
                        series.ChartType = SeriesChartType.Line;

                        for (int i = 0; i < x1.Length; i++)
                        {
                            series.Points.AddXY(x1[i], y1[i].TotalMilliseconds);
                        }
                        StatusLabel.Text = "";
                        break;
                    }

                case 2:
                    StatusLabel.Text = "Calculation in progress...";
                    foreach (int keySize in new[] { 256, 512, 1024 })
                    {
                        (int[] x2, TimeSpan[] y2, TimeSpan[] ignore, double[] ignore2) = await Task.Run(() => RSATester.TimeAndGrowthFromMessageLength(keySize));

                        {
                            Series series = chart1.Series.Add($"{keySize} bit");
                            series.ChartType = SeriesChartType.Line;

                            for (int i = 0; i < x2.Length; i++)
                            {
                                series.Points.AddXY(x2[i], y2[i].TotalMilliseconds);
                            }
                        }
                    }
                    StatusLabel.Text = "";
                    break;

                case 3:
                    StatusLabel.Text = "Calculation in progress...";
                    foreach (int keySize in new[] { 256, 512, 1024 })
                    {
                        (int[] x3, TimeSpan[] ignore3, TimeSpan[] y3, double[] ignore4) = await Task.Run(() => RSATester.TimeAndGrowthFromMessageLength(keySize));
                        {
                            Series series = chart1.Series.Add($"{keySize} bit");
                            series.ChartType = SeriesChartType.Line;

                            for (int i = 0; i < x3.Length; i++)
                            {
                                series.Points.AddXY(x3[i], y3[i].TotalMilliseconds);
                            }
                        }
                    }
                    StatusLabel.Text = "";
                    break;

                case 4:
                    StatusLabel.Text = "Calculation in progress...";
                    foreach (int keySize in new[] { 256, 512, 1024 })
                    {
                        (int[] x4, TimeSpan[] ignore5, TimeSpan[] ignore6, double[] y4) = await Task.Run(() => RSATester.TimeAndGrowthFromMessageLength(keySize));
                        {
                            Series series = chart1.Series.Add($"{keySize} bit");
                            series.ChartType = SeriesChartType.Line;

                            for (int i = 0; i < x4.Length; i++)
                            {
                                series.Points.AddXY(x4[i], y4[i]);
                            }
                        }
                    }
                    StatusLabel.Text = "";
                    break;

                default:
                    break;
            }

        }

        private void GetKeysButton_Click(object sender, EventArgs e)
        {
            testsOutputRichTextBox.Clear();
            MyRSA rsa = new MyRSA(128, 65537);
            testsOutputRichTextBox.Text += "128:\n";
            testsOutputRichTextBox.Text += $"Module: {rsa.GetPublicKey().Item2}\nOpen exponent: {rsa.GetPublicKey().Item1}\nClosed exponent: {rsa.GetPrivateKey().Item1}\n\n";

            rsa = new MyRSA(256, 65537);
            testsOutputRichTextBox.Text += "256:\n";
            testsOutputRichTextBox.Text += $"Module: {rsa.GetPublicKey().Item2}\nOpen exponent: {rsa.GetPublicKey().Item1}\nClosed exponent: {rsa.GetPrivateKey().Item1}\n\n";

            rsa = new MyRSA(512, 65537);
            testsOutputRichTextBox.Text += "512:\n";
            testsOutputRichTextBox.Text += $"Module: {rsa.GetPublicKey().Item2}\nOpen exponent: {rsa.GetPublicKey().Item1}\nClosed exponent: {rsa.GetPrivateKey().Item1}\n\n";
        }
    }
}
