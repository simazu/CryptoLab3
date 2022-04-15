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
            for (keySize = 70; watch.Elapsed.TotalSeconds < 10; keySize += 10)
            {
                MyRSA rsa = new MyRSA(keySize, 3);
                BigInteger module = rsa.GetPublicKey().Item2;
                GC.Collect();
                watch.Restart();
                Algorithms.PollardAlgorithm(module);
                watch.Stop();
                keySizes.Add(keySize);
                factorizationTimes.Add(watch.Elapsed);
            }
            for (; watch.Elapsed.TotalSeconds < 20; keySize+=2)
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
            const int keySize = 100;
            Stopwatch watch = new Stopwatch();
            double[] r = new double[10];
            TimeSpan[] factorizationTimes = new TimeSpan[10];
            for (int i = 0; i < 10; i++)
            {
                MyRSA rsa = new MyRSA((int) r[i] * keySize, (int) (1 - r[i]) * keySize);
                BigInteger module = rsa.GetPublicKey().Item2;
                GC.Collect();
                watch.Restart();
                Algorithms.PollardAlgorithm(module);
                watch.Stop();
                r[i] = 0.25 + 0.025 * i;
                factorizationTimes[i] = watch.Elapsed;
            }
            return (r, factorizationTimes);
        }

        /// <summary>
        /// keySizes: 256, 512, 1024 
        /// </summary>
        /// <returns>messageLengthInBytes, encryptionTimes, decryptionTimes, growthFactor</returns>
        public static (int[], TimeSpan[][], TimeSpan[][], double[][]) TimeAndGrowthFromMessageLength()
        {
            string fileName = "longtext.txt";
            byte[] bytes = Converter.FileToBytes(fileName);
            int numSamples = 10;
            Stopwatch watch = new Stopwatch();
            int[] messageLengthInBytes = new int[numSamples];
            TimeSpan[][] encryptionTimes = new TimeSpan[3][];
            TimeSpan[][] decryptionTimes = new TimeSpan[3][];
            double[][] growthFactor = new double[3][];
            for(int i = 0; i < 3; i++)
            {
                MyRSA rsa = new MyRSA(256 * (int)Math.Pow(2, i));
                encryptionTimes[i] = new TimeSpan[numSamples];
                decryptionTimes[i] = new TimeSpan[numSamples];
                growthFactor[i] = new double[numSamples];
                for (int j = 0; j < numSamples; j++)
                {
                    messageLengthInBytes[j] = bytes.Length * j / numSamples;
                    byte[] subbytes = new byte[messageLengthInBytes[j]];
                    Array.Copy(bytes, subbytes, messageLengthInBytes[j]);

                    GC.Collect();
                    watch.Restart();
                    string[] encryptedText = rsa.Encrypt(subbytes);
                    watch.Stop();
                    encryptionTimes[i][j] = watch.Elapsed;

                    rsa.Encrypt(fileName);
                    growthFactor[i][j] = Converter.FileToBytes("encrypted.txt").Length / bytes.Length;

                    GC.Collect();
                    watch.Restart();
                    rsa.Decrypt(encryptedText);
                    watch.Stop();
                    decryptionTimes[i][j] = watch.Elapsed;
                }
            }
            return (messageLengthInBytes, encryptionTimes, decryptionTimes, growthFactor);
        }


    }
}
