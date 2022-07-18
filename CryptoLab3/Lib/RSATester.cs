using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace CryptoLab3.Lib
{
    public static class RSATester
    {
        public static (int[], TimeSpan[]) RSAAttack()
        {
            Stopwatch watch = new Stopwatch();
            List<int> keySizes = new List<int>();
            List<TimeSpan> factorizationTimes = new List<TimeSpan>();
            int keySize;
            for (keySize = 70; watch.Elapsed.TotalSeconds < 40; keySize += 10)
            {
                MyRSA rsa = new MyRSA(keySize);
                BigInteger module = rsa.GetPublicKey().Item2;
                GC.Collect();
                watch.Restart();
                Algorithms.PollardAlgorithm(module);
                watch.Stop();
                keySizes.Add(keySize);
                factorizationTimes.Add(watch.Elapsed);
            }
            for (; watch.Elapsed.TotalSeconds < 60; keySize+=2)
            {
                MyRSA rsa = new MyRSA(keySize);
                BigInteger module = rsa.GetPublicKey().Item2;
                GC.Collect();
                watch.Restart();
                Algorithms.PollardAlgorithm(module);
                watch.Stop();
                keySizes.Add(keySize);
                factorizationTimes.Add(watch.Elapsed);
            }
            return (keySizes.ToArray(), factorizationTimes.ToArray());
        }

        public static (double[], TimeSpan[]) FactorizationTimeFromPQDifference()
        {
            const int keySize = 90;
            Stopwatch watch = new Stopwatch();
            double[] r = new double[10];
            TimeSpan[] factorizationTimes = new TimeSpan[10];
            for (int i = 0; i < 10; i++)
            {
                r[i] = 0.25 + 0.025 * i;
                MyRSA rsa = new MyRSA((int) (r[i] * keySize), (int) ((1 - r[i]) * keySize), 65537);

                BigInteger module = rsa.GetPublicKey().Item2;
                GC.Collect();
                watch.Restart();
                Algorithms.PollardAlgorithm(module);
                watch.Stop();            
                
                factorizationTimes[i] = watch.Elapsed;
            }
            return (r, factorizationTimes);
        }

        /// <summary>
        /// keySizes: 256, 512, 1024 
        /// </summary>
        /// <returns>messageLengthInBytes, encryptionTimes, decryptionTimes, growthFactor</returns>
        public static (int[], TimeSpan[], TimeSpan[], double[]) TimeAndGrowthFromMessageLength(int keySize)
        {
            int numSamples = 30; 

            int[] messageLengthInBytes = new int[numSamples];
            TimeSpan[] encryptionTimes = new TimeSpan[numSamples];
            TimeSpan[] decryptionTimes = new TimeSpan[numSamples];
            double[] growthFactor = new double[numSamples];

            string fileName = "longtext.txt";
            byte[] bytes = Converter.FileToBytes(fileName);
            Stopwatch watch = new Stopwatch();
            MyRSA rsa = new MyRSA(keySize);
            
            for (int j = 0; j < numSamples; j++)
            {
                messageLengthInBytes[j] = bytes.Length * (j + 1) / numSamples;
                byte[] subbytes = new byte[messageLengthInBytes[j]];
                Array.Copy(bytes, subbytes, messageLengthInBytes[j]);

                GC.Collect();
                watch.Restart();
                string[] encryptedText = rsa.Encrypt(subbytes);
                watch.Stop();
                encryptionTimes[j] = watch.Elapsed;

                int encryptedLength = 0;
                foreach (string line in encryptedText)
                    encryptedLength += Encoding.UTF8.GetByteCount(line);
                growthFactor[j] = encryptedLength / subbytes.Length;

                GC.Collect();
                watch.Restart();
                rsa.Decrypt(encryptedText);
                watch.Stop();
                decryptionTimes[j] = watch.Elapsed;
            }
            return (messageLengthInBytes, encryptionTimes, decryptionTimes, growthFactor);
        }


    }
}
