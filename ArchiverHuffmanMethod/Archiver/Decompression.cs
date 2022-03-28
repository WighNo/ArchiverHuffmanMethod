using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiverHuffmanMethod
{
    public class Decompression
    {
        private Calculations _calculations = new Calculations();

        public void Execute(string filePath, string dataFileName)
        {
            byte[] data = File.ReadAllBytes(filePath);
            byte[] archive = DecompressBytes(data);
            File.WriteAllBytes(dataFileName, archive);
        }

        private byte[] DecompressBytes(byte[] archive)
        {
            ParseHeader(archive, out int dataLength, out int startIndex, out int[] freqs);

            Node rootNode = _calculations.CreateTree(freqs);
            byte[] data = Decompress(archive, dataLength, startIndex, rootNode);

            return data;
        }

        private byte[] Decompress(byte[] archive, int dataLength, int startIndex, Node rootNode)
        {
            int size = 0;

            Node currentNode = rootNode;

            List<byte> data = new List<byte>();

            for (int i = startIndex; i < archive.Length; i++)
            {
                for (int bit = 1; bit <= 128; bit <<= 1)
                {
                    bool zero = (archive[i] & bit) == 0;

                    if (zero == true)
                        currentNode = currentNode.Bit0;
                    else
                        currentNode = currentNode.Bit1;

                    if (currentNode.Bit0 is null == false)
                        continue;

                    if (size++ < dataLength == true)
                        data.Add(currentNode.Symbol);

                    currentNode = rootNode;
                }
            }
            return data.ToArray();
        }

        private void ParseHeader(byte[] archive, out int dataLength, out int startIndex, out int[] freqs)
        {
            dataLength = archive[0] | (archive[1] << 8) | (archive[1] << 16) | (archive[1] << 24);

            freqs = new int[256];

            for (int i = 0; i < 256; i++)
                freqs[i] = archive[4 + i];

            startIndex = 4 + 256;
        }




    }
}
