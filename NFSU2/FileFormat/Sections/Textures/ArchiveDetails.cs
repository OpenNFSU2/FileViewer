using NFSU2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.FileFormat.Sections.Textures
{
    [Section(Header = SectionHeaders.ArchiveDetails)]
    public class ArchiveDetails : ISection
    {
        public string FileName { get; private set; }
        public string SourceName { get; private set; }
        public int Unknown { get; private set; }

        public bool Parse(Section section)
        {
            var reader = new BinaryReader(new MemoryStream(section.Data));

            // 4 byte - unknown
            reader.ReadInt32();

            // null terminated file name
            FileName = reader.ReadNullTerminatedString();
            
            // move to next string
            reader.BaseStream.Position = 32;

            // some sort of source name while compiling archive
            SourceName = reader.ReadNullTerminatedString();

            // move to unknown id/hash
            reader.BaseStream.Position = 96;

            // read some sort of hash, appears in unknown block before compressed data
            Unknown = reader.ReadInt32();

            return true;
        }
    }
}
