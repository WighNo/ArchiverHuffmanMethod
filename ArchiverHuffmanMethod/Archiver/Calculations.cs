using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiverHuffmanMethod
{
    class Calculations
    {
        public int[] Freq(byte[] data)
        {
            int[] freqs = new int[256];

            foreach (byte b in data)
                freqs[b]++;

            NormalizeFreqs(freqs);
            return freqs;
        }

        private void NormalizeFreqs(int[] freqs)
        {
            int max = freqs.Max();

            if (max <= 255)
                return;

            for(int i = 0; i < 256; i++)
            {
                if(freqs[i] > 0)
                {
                    freqs[i] = 1 + freqs[i] * 255 / (max + 1);
                }
            }
        }

        public Node CreateTree(int[] freqs)
        {
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();

            for (int i = 0; i < 256; i++)
            {
                if (freqs[i] > 0)
                    priorityQueue.Enqueue(freqs[i], new Node((byte)i, freqs[i]));
            }

            while (priorityQueue.Size > 1)
            {
                Node bit0 = priorityQueue.Dequeue();
                Node bit1 = priorityQueue.Dequeue();

                int freq = bit0.Freq + bit1.Freq;

                Node next = new Node(bit0, bit1, freq);

                priorityQueue.Enqueue(freq, next);
            }

            return priorityQueue.Dequeue();
        }
    }
}
