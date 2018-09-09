using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.FileFormat.Sections
{
    [Section(Header = SectionHeaders.MeshVertices)]
    public class MeshVertices : ISection
    {
        public struct Vector3f
        {
            public float X;
            public float Y;
            public float Z;

            public Vector3f(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }

        public struct Vector2f
        {
            public float X;
            public float Y;

            public Vector2f(float x, float y)
            {
                X = x;
                Y = y;
            }
        }

        public List<Vector3f> Positions { get; } = new List<Vector3f>();
        public List<Vector3f> Normals { get; } = new List<Vector3f>();
        public List<Vector2f> TexCoordinates { get; } = new List<Vector2f>();
        public List<float> Unknown { get; } = new List<float>();

        public bool Parse(Section section)
        {
            var reader = new BinaryReader(new MemoryStream(section.Data));
            reader.BaseStream.Position = 116;

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                var posX = reader.ReadSingle();
                var posY = reader.ReadSingle();
                var posZ = reader.ReadSingle();

                var normalX = reader.ReadSingle();
                var normalY = reader.ReadSingle();
                var normalZ = reader.ReadSingle();

                var unknown = reader.ReadSingle();

                var texCoordX = reader.ReadSingle();
                var texCoordY = reader.ReadSingle();

                Positions.Add(new Vector3f(posX, posY, posZ));
                Normals.Add(new Vector3f(normalX, normalY, normalZ));
                TexCoordinates.Add(new Vector2f(texCoordX, texCoordY));
                Unknown.Add(unknown);
            }

            return true;
        }
    }
}
