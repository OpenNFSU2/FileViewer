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
            reader.BaseStream.Position = 12;

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                Indices.Add(reader.ReadUInt16());
            }

            return true;
        }
    }
}
