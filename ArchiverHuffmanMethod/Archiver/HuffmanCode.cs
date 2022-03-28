using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiverHuffmanMethod
{
    class HuffmanCode
    {
        private string[] _codes;

        public string[] CreateCode(Node rootNode)
        {
            _codes = new string[256];

            Next(rootNode, "");

            return _codes;
        }

        private void Next(Node node, string code)
        {
            if(node.Bit0 is null == true)
            {
                _codes[node.Symbol] = code;
            }
            else
            {
                Next(node.Bit0, code + "0");
                Next(node.Bit1, code + "1");
            }
        }
    }
}
