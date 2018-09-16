using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.FileFormat.Sections.Textures
{
    [Section(Header = SectionHeaders.FileList)]
    public class FileList : ISection
    {
        public List<int> Files { get; } = new List<int>(); 

        public bool Parse(Section section)
        {
            var reader = new BinaryReader(new MemoryStream(section.Data));

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                // read file hash/id
                var hash = reader.ReadInt32();

                // 4 byte unkown (seems to be always 0)
                reader.ReadInt32();

                Files.Add(hash);
            }

            return true;
        }
    }
}
