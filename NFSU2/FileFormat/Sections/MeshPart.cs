using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.FileFormat.Sections
{
    [Section(Header = SectionHeaders.MeshPart)]
    public class MeshPart : ISection
    {
        public MeshInfo Info { get; private set; }
        public MeshDescription Description { get; private set; }
        public MeshVertices Vertices { get; private set; }
        public MeshIndices Indices { get; private set; }

        public bool Parse(Section section)
        {
            Info = section.GetSection(SectionHeaders.MeshInfo).Decode<MeshInfo>();

            var meshData = section.GetSection(SectionHeaders.MeshData);

            Description = meshData.GetSection(SectionHeaders.MeshDescription).Decode<MeshDescription>();
            Vertices = meshData.GetSection(SectionHeaders.MeshVertices).Decode<MeshVertices>();
            Indices = meshData.GetSection(SectionHeaders.MeshIndices).Decode<MeshIndices>();

            return true;
        }
    }
}
