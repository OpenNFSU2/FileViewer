using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.FileFormat.Sections
{
    [Section(Header = SectionHeaders.MeshIndices)]
    public class MeshIndices : ISection
    {
        public List<ushort> Indices { get; } = new List<ushort>();

        public bool Parse(Section section)
        {
            var reader = new BinaryReader(new MemoryStream(section.Data));
            while (reader.ReadByte() == 0x11) { }
            reader.BaseStream.Position -= 1;

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                Indices.Add(reader.ReadUInt16());
            }

            return true;
        }
    }
}
