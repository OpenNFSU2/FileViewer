using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.FileFormat.Sections.Textures
{
    [Section(Header = SectionHeaders.FileDictionary)]
    public class FileDictionary : ISection
    {
        public struct FileDetails
        {
            public int Hash;
            public uint Offset;
            public uint CompressedLength;
            public uint DecompressedLength;
        }

        public List<FileDetails> Files { get; } = new List<FileDetails>();

        public bool Parse(Section section)
        {
            var reader = new BinaryReader(new MemoryStream(section.Data));

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                var hash = reader.ReadInt32();
                if (hash == 0) break;

                var offset = reader.ReadUInt32();
                var compressedLength = reader.ReadUInt32();
                var decompressedLength = reader.ReadUInt32();

                // 4 byte unknown
                reader.ReadInt32();

                // 4 byte unknown - seems to be always null/zero
                reader.ReadInt32();

                Files.Add(new FileDetails
                {
                    Hash = hash,
                    Offset = offset,
                    CompressedLength = compressedLength,
                    DecompressedLength = decompressedLength
                });
            }

            return true;
        }
    }
}
