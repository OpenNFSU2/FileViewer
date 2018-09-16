using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.Utils
{
    public static class BinaryReaderExtensions
    {
        public static string ReadNullTerminatedString(this BinaryReader reader)
        {
            var ret = "";
            byte b;
            while ((b = reader.ReadByte()) != 0x00)
            {
                ret += (char) b;
            }

            reader.BaseStream.Position -= 1;

            return ret;
        }
    }
}
