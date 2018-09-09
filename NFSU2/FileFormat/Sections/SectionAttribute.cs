using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSU2.FileFormat.Sections
{
    [AttributeUsage(AttributeTargets.Class)]
    class SectionAttribute : Attribute
    {
        public SectionHeaders Header { get; set; }
    }
}
