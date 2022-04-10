using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace CryptoLab3.Lib
{
    public static class Converter
    {
        public static string ToString(BitArray sequence)
        {
            string result = "";
            for (int i = 0; i < sequence.Length; i++)
                result += sequence[i] ? "1" : "0";

            return result;
        }

        public static BitArray ToBitArray(string sequence)
        {
            BitArray result = new BitArray(sequence.Length);
            for (int i = 0; i < sequence.Length; i++)
                result[i] = sequence[i] == '1';
            return result;
        }

        public static byte[] ToByteArray(BitArray bits)
        {
            int numBytes = bits.Count / 8;
            if (bits.Count % 8 != 0) numBytes++;

            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));

                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }
            return bytes;
        }

        public static string FileToBits(string fileName)
        {
            byte[] bytes = FileToBytes(fileName);

            BitArray fileBits = new BitArray(bytes);

            for (int i = 0; i < fileBits.Length; i += 8)
            {
                for (int j = 0; j < 4; j++)
                {
                    (fileBits[i + j], fileBits[i + 7 - j]) = (fileBits[i + 7 - j], fileBits[i + j]);
                }
            }

            return Converter.ToString(fileBits);
        }

        public static byte[] FileToBytes(string fileName)
        {
            using FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            // Read the source file into a byte array.
            byte[] bytes = new byte[fileStream.Length];
            int numBytesToRead = (int)fileStream.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                // Read may return anything from 0 to numBytesToRead.
                int n = fileStream.Read(bytes, numBytesRead, numBytesToRead);

                // Break when the end of the file is reached.
                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
            return bytes;
        }
    }
}
