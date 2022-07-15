using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace CryptoLab3.Lib
{
    public static class Algorithms
    {
        /// <summary>
        /// Returns Reatest Commaon Divisor of a and b
        /// </summary>
        public static BigInteger EuclideanAlgorithm(BigInteger a, BigInteger b)
        {
            while (b != 0)
            {
                BigInteger temp = a % b;
                a = b;
                b = temp;
            }
            return a;
        }

        public static BigInteger FastExponentiationAlgorithm(BigInteger basis, BigInteger power, BigInteger mod)
        {
            BigInteger result = 1;
            while (power > 0)
            {
                if (!power.IsEven)
                    result = (result * basis) % mod;
                power /= 2;
                basis = (basis * basis) % mod;
            }
            return result;
        }

        public static (BigInteger, BigInteger, BigInteger) ExtendedEuclideanAlgorithm(BigInteger a, BigInteger b)
        {
            BigInteger u1 = 1, u2 = 0, v1 = 0, v2 = 1;
            while (b != 0)
            {
                BigInteger q = a / b;
                BigInteger r = a % b;
                a = b;
                b = r;
                r = u2;
                u2 = u1 - q * u2;
                u1 = r;
                r = v2;
                v2 = v1 - q * v2;
                v1 = r;
            }
            return (a, u1, v1);
        }

        public static BigInteger GetSimpleRandomNumber(int lengthInBits)
        {
            int lengthInBytes = lengthInBits / 8;
            int k = 10;
            Random random = new Random();
            byte[] simpleRandomInBytes = new byte[lengthInBytes + 1];
            byte[] rndInBytes = new byte[lengthInBytes + 1];
            BigInteger rnd;
            BigInteger simpleRandom = 0;

            for (int i = 0; i < k;)
            {
                i = 0;
                random.NextBytes(simpleRandomInBytes);
                simpleRandomInBytes[lengthInBytes] = (byte)(simpleRandomInBytes[lengthInBytes] & 127);
                simpleRandomInBytes[lengthInBytes] = (byte)(simpleRandomInBytes[lengthInBytes] | 64);
                simpleRandom = new BigInteger(simpleRandomInBytes) >> 7; // чтобы битовая длина была правильной

                while (i < k)
                {
                    random.NextBytes(rndInBytes);
                    rndInBytes[lengthInBytes] = (byte)(rndInBytes[lengthInBytes] & 63);
                    rnd = (new BigInteger(rndInBytes) >> 7) + 1;

                    if (FastExponentiationAlgorithm(rnd, simpleRandom - 1, simpleRandom) == 1)
                        i++;
                    else
                        break;
                }
            }
            return simpleRandom;
        }

        /// <summary>
        /// Returns one of dividers of n
        /// </summary>
        public static BigInteger PollardAlgorithm(BigInteger n)
        {
            List<BigInteger> x = new List<BigInteger> { GetSimpleRandomNumber((int)n.GetBitLength()-1),
                                                        GetSimpleRandomNumber((int)n.GetBitLength()-1)};
            while (true)
            {
                if (((BigInteger)x.Count).IsPowerOfTwo)
                    for (int i = 1; i < x.Count; i++)
                    {
                        BigInteger gcd = EuclideanAlgorithm(n, BigInteger.Abs(x.Last() - x[i]));
                        if (gcd != n && gcd > 1)
                            return gcd;
                    }
                x.Add((x.Last() * x.Last() + 1) % n);
            }
        }


    }
}
