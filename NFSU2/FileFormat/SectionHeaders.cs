using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.FileFormat
{
    public enum SectionHeaders : uint
    {
        MeshPart = 0x80134010,
        MeshData = 0x80134100,

        MeshInfo = 0x00134011,
        MeshDescription = 0x00134900,
        MeshVertices = 0x00134b01,
        MeshIndices = 0x00134b03,
    }
}
