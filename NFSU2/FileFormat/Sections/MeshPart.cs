using System;
using System.Collections.Generic;
using System.Globalization;
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

        public void WriteObjFile(StreamWriter writer, uint verticesOffset = 0, string namePostfix = "")
        {
            writer.WriteLine($"o {Info.Name}{namePostfix}");

            foreach (var vertex in Vertices.Positions)
            {
                writer.WriteLine($"v {vertex.X.ToString(CultureInfo.InvariantCulture)} {vertex.Y.ToString(CultureInfo.InvariantCulture)} {vertex.Z.ToString(CultureInfo.InvariantCulture)}");
            }

            writer.WriteLine();

            foreach (var normal in Vertices.Normals)
            {
                writer.WriteLine($"vn {normal.X.ToString(CultureInfo.InvariantCulture)} {normal.Y.ToString(CultureInfo.InvariantCulture)} {normal.Z.ToString(CultureInfo.InvariantCulture)}");
            }

            writer.WriteLine();

            foreach (var texCoord in Vertices.TexCoordinates)
            {
                writer.WriteLine($"vt {texCoord.X.ToString(CultureInfo.InvariantCulture)} {texCoord.Y.ToString(CultureInfo.InvariantCulture)}");
            }

            writer.WriteLine();

            for (var i = 0; i < Description.TriangleCount; i += 3)
            {
                writer.WriteLine($"f {Indices.Indices[i + 0] + 1 + verticesOffset} {Indices.Indices[i + 1] + 1 + verticesOffset} {Indices.Indices[i + 2] + 1 + verticesOffset}");
            }
        }
    }
}
