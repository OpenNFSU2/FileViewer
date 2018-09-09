using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.FileFormat.Sections
{
    [Section(Header = SectionHeaders.MeshInfo)]
    public class MeshInfo : ISection
    {
        public string Name { get; private set; }

        public bool Parse(Section section)
        {
            var position = 164; // name start at 164
            var name = "";

            while (section.Data[position] != 0x00)
            {
                name += (char)section.Data[position];
                position++;
            }

            Name = name;

            return true;
        }
    }
}
