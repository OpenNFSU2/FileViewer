using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.Compression
{
    public class UnknownCompressionMethod : Exception
    {
        public UnknownCompressionMethod(byte[] header) : base("Unknown compression method, header: " + BitConverter.ToString(header))
        {

        }
    }
}
