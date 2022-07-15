using System;
using System.IO;
using System.Numerics;

namespace CryptoLab3.Lib
{
    public class MyRSA
    {
        int KeySize;
        int OpenExponent;
        BigInteger P;
        BigInteger Q;

        BigInteger EulerFunction() => (P-1)*(Q-1);
        BigInteger Module() => P*Q;
        BigInteger ClosedExponent() => Algorithms.ExtendedEuclideanAlgorithm(OpenExponent, EulerFunction()).Item2;

        public MyRSA(int keySize = 1024, int openExponent = 65537)
        {
            KeySize = keySize;
            OpenExponent = openExponent;
            while (ClosedExponent() <= 0)
            {
                P = Algorithms.GetSimpleRandomNumber(KeySize / 2);
                Q = Algorithms.GetSimpleRandomNumber(KeySize / 2);
            }
        }
      
        public MyRSA(int pLength, int qLength, int openExponent = 65537)
        {
            P = Algorithms.GetSimpleRandomNumber(pLength);
            Q = Algorithms.GetSimpleRandomNumber(qLength); 
            OpenExponent = openExponent;
            KeySize = pLength + qLength;
        }

        public MyRSA(BigInteger p, BigInteger q, int openExponent = 65537)
        {
            P = p;
            Q = q; ;
            OpenExponent = openExponent;
            KeySize = (int)(P.GetBitLength() + Q.GetBitLength());
        }

        public (int, BigInteger) GetPublicKey() => (OpenExponent, Module());
        public (BigInteger, BigInteger) GetPrivateKey() => (ClosedExponent(), Module());
        public void Encrypt(string fileName) => Encrypt(fileName, (OpenExponent, Module()));
        public string[] Encrypt(byte[] bytes) => Encrypt(bytes, (OpenExponent, Module()));
        public void Decrypt(string fileName) => Decrypt(fileName, (ClosedExponent(), Module()));
        public byte[] Decrypt(string[] encryptedText) => Decrypt(encryptedText, (OpenExponent, Module()));


        public static void Encrypt(string fileName, (int, BigInteger) publicKey)
        {
            byte[] bytes = Converter.FileToBytes(fileName);
            string[] encryptedText = Encrypt(bytes, publicKey);
            StreamWriter sw = new StreamWriter("encrypted.txt");
            foreach (string line in encryptedText)
                sw.WriteLine(line);
            sw.Close();
        }

        public static string[] Encrypt(byte[] bytes, (int, BigInteger) publicKey)
        {
            int blocksLengthInBytes = (int)publicKey.Item2.GetBitLength() / 4 / 8;
            int blocksCount = bytes.Length / blocksLengthInBytes;
            string[] encryptedText = new string[blocksCount];
            for (int i = 0; i < blocksCount; i++)
            {
                byte[] blockInBytes = new byte[blocksLengthInBytes];
                Array.Copy(bytes, i * blocksLengthInBytes, blockInBytes, 0, blocksLengthInBytes);
                if ((blockInBytes[blockInBytes.Length - 1] & 0x80) > 0)
                {
                    byte[] temp = new byte[blockInBytes.Length];
                    Array.Copy(blockInBytes, temp, blockInBytes.Length);
                    blockInBytes = new byte[temp.Length + 1];
                    Array.Copy(temp, blockInBytes, temp.Length);
                }
                BigInteger block = new BigInteger(blockInBytes);
                encryptedText[i] = Algorithms.FastExponentiationAlgorithm(block, publicKey.Item1, publicKey.Item2).ToString();
            }
            return encryptedText;
        }

        public static void Decrypt(string fileName, (BigInteger, BigInteger) privateKey)
        {
            string[] encryptedText = File.ReadAllLines(fileName);
            byte[] decryptedBytes = Decrypt(encryptedText, privateKey);
            using FileStream fsNew = new FileStream("decrypted.txt", FileMode.Create, FileAccess.Write);
            fsNew.Write(decryptedBytes, 0, decryptedBytes.Length);
        }

        public static byte[] Decrypt(string[] encryptedText, (BigInteger, BigInteger) privateKey)
        {
            int blocksLengthInBytes = (int)privateKey.Item2.GetBitLength() / 4 / 8;
            byte[] decryptedBytes = new byte[blocksLengthInBytes * encryptedText.Length];
            BigInteger block;
            for (int i = 0; i < encryptedText.Length; i++)
            {
                block = BigInteger.Parse(encryptedText[i]);
                byte[] decryptedBlock = Algorithms.FastExponentiationAlgorithm(block, privateKey.Item1, privateKey.Item2).ToByteArray();
                try
                {
                    Array.Copy(decryptedBlock, 0, decryptedBytes, i * blocksLengthInBytes, blocksLengthInBytes);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return decryptedBytes; 
        }
    }
}
