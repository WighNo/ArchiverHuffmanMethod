using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiverHuffmanMethod
{
    class Node
    {
        public readonly byte Symbol;
        public readonly int Freq;
        public readonly Node Bit0;
        public readonly Node Bit1;

        public Node(Node bit0, Node bit1, int freq)
        {
            Bit0 = bit0;
            Bit1 = bit1;
            Freq = freq;
        }

        public Node(byte symbol, int freq)
        {
            Symbol = symbol;
            Freq = freq;
        }
    }
}
