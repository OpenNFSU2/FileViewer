using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.FileFormat
{
    public enum SectionHeaders : uint
    {
        // GEOMETRY.BIN
        MeshPart = 0x80134010,
        MeshData = 0x80134100,
        MeshInfo = 0x00134011,
        MeshDescription = 0x00134900,
        MeshVertices = 0x00134b01,
        MeshIndices = 0x00134b03,

        // TEXTURES.BIN
        TextureArchive = 0xB3300000,
        ArchiveHeader = 0xB3310000,
        ArchiveDetails = 0x33310001,
        FileList = 0x33310002,
        FileDictionary = 0x33310003,
        ArchiveData = 0xB3320000,
        CompressedData = 0x33320002,
    }
}
