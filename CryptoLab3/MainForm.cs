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

namespace CryptoLab3
{
    public partial class MainForm : Form
    {
        const string PublicKeyFilePath = "publicKey.txt";
        const string PrivateKeyFilePath = "privateKey.txt";
        public MainForm()
        {
            InitializeComponent();
        }

        private void testsButton_Click(object sender, EventArgs e)
        {
            testsOutputRichTextBox.Clear();
            foreach (int openExponent in new[] {3, 65537 })
            {
                testsOutputRichTextBox.Text += $"\n____________________________\nencrypted text test, e = {openExponent}\n____________________________\n\n";
                MyRSA rsa = new MyRSA(128, openExponent);
                rsa.Encrypt("text.txt");

                string encryptedText = Converter.FileToBits("encrypted.txt");
                Test(encryptedText);
            }
        }



        private void Test(string sequence)
        {
            for (int i = 2; i < 5; i++)
            {
                (double, double, double) serialTestResult = MSequenceTester.SerialTest(sequence, i);
                testsOutputRichTextBox.Text += $"Serial test, serial length = {i}\n{serialTestResult.Item3} - AlphaMax: {serialTestResult.Item1}, AlphaMin: {serialTestResult.Item2}" +
                    $", test {((serialTestResult.Item3 <= serialTestResult.Item1 && serialTestResult.Item1 >= serialTestResult.Item2)?"passed":"failed")}\n";
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
                    $", test {(correlationTestResult.Item1<correlationTestResult.Item2?"passed":"failed")}\n";
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
            testsOutputRichTextBox.Text += "encrypted";
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            string fileName = "encrypted.txt";
            StreamReader sr = new StreamReader(PrivateKeyFilePath);
            (BigInteger, BigInteger) privateKey = (BigInteger.Parse(sr.ReadLine()), BigInteger.Parse(sr.ReadLine()));
            sr.Close();
            MyRSA.Decrypt(fileName, privateKey);
            testsOutputRichTextBox.Clear();
            testsOutputRichTextBox.Text += "decrypted";
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

        private void plotButton_Click(object sender, EventArgs e)
        {

            switch (chartComboBox.SelectedIndex)
            {
                case 0:
                    (int[] x0, TimeSpan[] y0) = RSATester.RSAAttack();
                    //TODO 
                    break;

                case 1:
                    (double[] x1, TimeSpan[] y1) = RSATester.FactorizationTimeFromPQDifference();
                    //TODO 
                    break;

                case 2:
                    foreach (int keySize in new[] {256, 512, 1024 })
                    {
                        (int[] x2, TimeSpan[] y2, TimeSpan[] ignore, double[] ignore2) = RSATester.TimeAndGrowthFromMessageLength(keySize);
                        //TODO Plot(x2, y2);
                    }
                    break;

                case 3:
                    foreach (int keySize in new[] { 256, 512, 1024 })
                    {
                        (int[] x3, TimeSpan[] ignore3, TimeSpan[] y3, double[] ignore4) = RSATester.TimeAndGrowthFromMessageLength(keySize);
                        //TODO Plot(x3, y3);
                    }
                    break;

                case 4:
                    foreach (int keySize in new[] { 256, 512, 1024 })
                    {
                        (int[] x4, TimeSpan[] ignore5, TimeSpan[] ignore6, double[] y4) = RSATester.TimeAndGrowthFromMessageLength(keySize);
                        //TODO Plot(x4, y4);
                    }
                    break;

                default:
                    break;
            }
                
        }
    }
}
