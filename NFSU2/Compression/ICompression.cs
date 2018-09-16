using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.Compression
{
    interface ICompression
    {
        byte[] Decompress(byte[] input);
    }
}
