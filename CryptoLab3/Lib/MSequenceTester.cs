using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLab3.Lib
{
    public static class MSequenceTester
    {
        public static (double, double, double) SerialTest(string sequence, int serialLenght)
        {
            double alphaMin, alphaMax;

            switch (serialLenght)
            {
                case 2:
                    alphaMin = 7.815;
                    alphaMax = 0.584;
                    break;
                case 3:
                    alphaMin = 14.067;
                    alphaMax = 2.833;
                    break;
                case 4:
                    alphaMin = 24.996;
                    alphaMax = 8.547;
                    break;
                default:
                    return (-1, -1, -1);
            }
            int count = 0;
            double referenceFrequency = sequence.Length / (serialLenght * Math.Pow(2, serialLenght));

            Dictionary<string, double> serialFrequencies = new Dictionary<string, double>();
            for (int i = 0; i < sequence.Length; i += serialLenght)
            {
                string serial;
                try
                {
                    serial = sequence.Substring(i, serialLenght);
                }
                catch
                {
                    continue;
                }

                if (serialFrequencies.Keys.Contains(serial))
                    serialFrequencies[serial]++;
                else
                    serialFrequencies.Add(serial, 1);

                count++;
            }

            double criteria = 0;
            foreach (double frequency in serialFrequencies.Values)
                criteria += Math.Pow(frequency - referenceFrequency, 2) / referenceFrequency;

            return (Math.Round(alphaMin, 5), Math.Round(alphaMax, 5), Math.Round(criteria, 5));
        }

        public static (double, double, double) PokerTest(string sequence)
        {
            double sequenceLength = sequence.Length;
            double[] P = new double[7];
            double[] Pref = new double[7];
            int q = 10;
            Dictionary<int, int>[] quentets = new Dictionary<int, int>[(int)sequenceLength / 32 / 5];  
            for (int i = 0;i < quentets.Length; i++)
            {
                quentets[i] = new Dictionary<int, int>();
                for (int j = 0;j < 5; j++)
                {
                    string subSequence;
                    try
                    {
                        subSequence = sequence.Substring(i * 32 * 5 + j * 32, 32);
                    }
                    catch
                    {
                        continue;
                    }

                    BitArray bitSubSequence = new BitArray(subSequence.Length);
                    for (int k = 0; k < subSequence.Length; k++)
                        bitSubSequence[k] = (subSequence[k] == '1' ? true : false);
                    double intSubSequence;
                    intSubSequence = BitConverter.ToInt32(Converter.ToByteArray(bitSubSequence), 0);
                    int u = (int)((intSubSequence / int.MaxValue) * q); 

                    if (quentets[i].ContainsKey(u))
                        quentets[i][u]++;
                    else
                        quentets[i].Add(u, 1);
                }

                if (quentets[i].Count == 5)
                    P[0]++;
                if (quentets[i].Count == 4)
                    P[1]++;
                if (quentets[i].Count == 3 && !quentets[i].ContainsValue(3))
                    P[2]++;
                if (quentets[i].Count == 3 && quentets[i].ContainsValue(3))
                    P[3]++;
                if (quentets[i].Count == 2 && !quentets[i].ContainsValue(4))
                    P[4]++;
                if (quentets[i].Count == 2 && quentets[i].ContainsValue(4))
                    P[5]++;
                if (quentets[i].Count == 1)
                    P[6]++;
            }
            
            Pref[0] = (q - 1) * (q - 2) * (q - 3) * (q - 4) / Math.Pow(q, 4);
            Pref[1] = 10 * (q - 1) * (q - 2) * (q - 3) / Math.Pow(q, 4);
            Pref[2] = 15 * (q - 1) * (q - 2) / Math.Pow(q, 4);
            Pref[3] = 10 * (q - 1) * (q - 2) / Math.Pow(q, 4);
            Pref[4] = 10 * (q - 1) / Math.Pow(q, 4);
            Pref[5] = 5 * (q - 1) / Math.Pow(q, 4);
            Pref[6] = 1 / Math.Pow(q, 4);

            for (int i = 0; i < 7; i++)
                Pref[i] = Pref[i] * quentets.Length;

            double criteria = 0;
            for (int i = 0; i < 7; i++)
                criteria += Math.Pow(P[i] - Pref[i], 2) / Pref[i];

            return (11.071, 1.61, Math.Round(criteria, 5));
        }

        public static (double, double) CorrelationTest(string sequence, int k)
        {
            double sequenceLength = sequence.Length;
            List<int> bitSequence = new List<int>();
            for (int i = 0; i < sequence.Length; i++)
                bitSequence.Add(sequence[i] == '1' ? 1 : 0);

            double m1 = 0;
            for (int i = 0; i < sequenceLength - k; i++)
                m1 += bitSequence[i];
            m1 /= (sequenceLength - k);

            double m2 = 0;
            for (int i = k; i < sequenceLength; i++)
                m2 += bitSequence[i];
            m2 /= (sequenceLength - k);

            double d1 = 0;
            for (int i = 0; i < sequenceLength - k; i++)
                d1 += Math.Pow((bitSequence[i] - m1), 2);
            d1 /= (sequenceLength - k - 1);

            double d2 = 0;
            for (int i = k; i < sequenceLength; i++)
                d2 += Math.Pow((bitSequence[i] - m2), 2);
            d2 /= (sequenceLength - k - 1);

            double R = 0;
            for (int i = 0; i < sequenceLength - k; i++)
                R += (bitSequence[i] - m1) * (bitSequence[i + k] - m2);
            R = Math.Abs(R) / (sequenceLength - k);
            R /= Math.Sqrt(d1 * d2);
            double Rref = 1 / (sequenceLength - 1) + (2 / (sequenceLength - 1)) * Math.Sqrt(sequenceLength * (sequenceLength - 3) / (sequenceLength + 1));
            
            return (Math.Round(R, 5), Math.Round(Rref, 5));
        }
    }
}
