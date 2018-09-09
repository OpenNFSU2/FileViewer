using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.FileFormat.Sections
{
    [Section(Header = SectionHeaders.MeshDescription)]
    class MeshDescription : ISection
    {
        public int TriangleCount { get; set; }
        public int VerticesCount { get; set; }

        public bool Parse(BinaryReader reader, long position)
        {
            reader.BaseStream.Position = position + 36;
            TriangleCount = reader.ReadInt32();

            reader.BaseStream.Position = position + 52;
            VerticesCount = reader.ReadInt32();

            return true;
        }
    }
}
