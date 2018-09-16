using System.IO;

namespace NFSU2.FileFormat.Sections.Geometry
{
    [Section(Header = SectionHeaders.MeshDescription)]
    public class MeshDescription : ISection
    {
        public int TriangleCount { get; set; }
        public int VerticesCount { get; set; }

        public bool Parse(Section section)
        {
            var reader = new BinaryReader(new MemoryStream(section.Data));
            while (reader.ReadByte() == 0x11) { }
            reader.BaseStream.Position -= 1;

            reader.BaseStream.Position += 36;
            TriangleCount = reader.ReadInt32();

            reader.BaseStream.Position += 12;
            VerticesCount = reader.ReadInt32();

            return true;
        }
    }
}
