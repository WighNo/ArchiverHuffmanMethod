using ArchiverHuffmanMethod;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiverHuffmanMethod.Compress
{
    public class Compression
    {
        private Calculations _calculations = new Calculations();

        public void Execute(string savePath, string archiveFileName)
        {
            byte[] data = File.ReadAllBytes(savePath);
            byte[] archive = CompressBytes(data);
            File.WriteAllBytes(archiveFileName, archive);
        }

        private byte[] CompressBytes(byte[] data)
        {
            int[] freqs = _calculations.Freq(data);

            Node rootNode = _calculations.CreateTree(freqs);
            HuffmanCode huffmanCode = new HuffmanCode();

            string[] codes = huffmanCode.CreateCode(rootNode);

            byte[] bits = Compress(data, codes);
            byte[] head = CreateHeader(data.Length, freqs);

            return head.Concat(bits).ToArray();
        }

        private byte[] CreateHeader(int dataLength, int[] freqs)
        {
            List<byte> head = new List<byte>();

            head.Add((byte)(dataLength & 255));
            head.Add((byte)((dataLength >> 8) & 255));
            head.Add((byte)((dataLength >> 16) & 255));
            head.Add((byte)((dataLength >> 24) & 255));

            for (int i = 0; i < 256; i++)
                head.Add((byte)freqs[i]);

            return head.ToArray();
        }

        private byte[] Compress(byte[] data, string[] codes)
        {
            List<byte> bits = new List<byte>();

            byte sum = 0;
            byte bit = 1;

            foreach (byte symbol in data)
            {
                foreach (char ch in codes[symbol])
                {
                    if (ch == '1')
                    {
                        sum |= bit;
                    }

                    if (bit < 128)
                    {
                        bit <<= 1;
                    }
                    else
                    {
                        bits.Add(sum);
                        sum = 0;
                        bit = 1;
                    }
                }
            }

            if (bit > 1)
            {
                bits.Add(sum);
            }

            return bits.ToArray();
        }
    }
}
