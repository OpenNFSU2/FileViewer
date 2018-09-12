using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Be.Windows.Forms;
using NFSU2.FileFormat.Sections;

namespace NFSU2.FileFormat
{
    public class Section : TreeNode, IByteProvider
    {
        public Section ParentSection { get; }

        public Stream Stream { get; private set; }
        public uint Header { get; private set; }
        public int Length { get; private set; }

        public List<Section> SubSections { get; private set; }
        public byte[] Data { get; private set; }

        public bool HasSubSections { get; private set; }

        long IByteProvider.Length => Length;

        public long Position { get; private set; }
        private BinaryReader _reader;

        public event EventHandler LengthChanged;
        public event EventHandler Changed;

        public Section(Stream stream, long position, Section parent = null)
        {
            ParentSection = parent;
            Stream = stream;
            Position = position;

            _reader = new BinaryReader(Stream);
            Stream.Position = position;

            Header = _reader.ReadUInt32();
            Length = _reader.ReadInt32();

            var headerBytes = BitConverter.GetBytes(Header);
            if ((headerBytes[3] & (1 << 7)) != 0)
            {
                HasSubSections = true;
                SubSections = new List<Section>();

                var read = 0;
                while (read < Length)
                {
                    var section = new Section(stream, Stream.Position, this);
                    read += 4 + 4 + section.Length;

                    SubSections.Add(section);
                    if(section.Header != 0) Nodes.Add(section);
                }
            }
            else
            {
                HasSubSections = false;
                Data = _reader.ReadBytes(Length);
            }

            Text = ToString();
        }

        public object Decode()
        {
            foreach (var type in GetType().Assembly.GetTypes())
            {
                var attributes = type.GetCustomAttributes(typeof(SectionAttribute), false);
                foreach (var attribute in attributes)
                {
                    if (!(attribute is SectionAttribute sectionAttr)) continue;
                    if ((uint) sectionAttr.Header != Header) continue;

                    var cl = Activator.CreateInstance(type);
                    if (cl is ISection sec)
                    {
                        sec.Parse(this);
                        return sec;
                    }
                }
            }

            return null;
        }

        public T Decode<T>() where T: ISection
        {
            var decoded = Decode();
            if (decoded is T t) return t;
            return default(T);
        }

        public Section GetSection(SectionHeaders header)
        {
            if (!HasSubSections) return null;

            foreach (var section in SubSections)
            {
                if (section.Header == (uint)header) return section;
            }

            return null;
        }

        public IEnumerable<Section> GetSections(SectionHeaders header)
        {
            if (!HasSubSections) yield break;

            foreach (var section in SubSections)
            {
                if (section.Header == (uint) header) yield return section;
            }
        }

        public sealed override string ToString()
        {
            var headerName = Header.ToString("X8");
            if (Enum.IsDefined(typeof(SectionHeaders), Header))
            {
                headerName = ((SectionHeaders)Header).ToString();
            }

            var str = headerName + " (" + Length + " bytes)";

            return str;
        }

        private string Prefix(string s)
        {
            var ret = "";
            var lines = s.Split('\n');
            foreach (var line in lines)
            {
                ret += "  " + line + "\n";
            }

            return ret;
        }

        public byte ReadByte(long index)
        {
            _reader.BaseStream.Position = Position + 8 + index;
            return _reader.ReadByte();
        }

        public void WriteByte(long index, byte value)
        {
            throw new NotImplementedException();
        }

        public void InsertBytes(long index, byte[] bs)
        {
            throw new NotImplementedException();
        }

        public void DeleteBytes(long index, long length)
        {
            throw new NotImplementedException();
        }

        public bool HasChanges()
        {
            return false;
        }

        public void ApplyChanges()
        {
            throw new NotImplementedException();
        }

        public bool SupportsWriteByte()
        {
            return false;
        }

        public bool SupportsInsertBytes()
        {
            return false;
        }

        public bool SupportsDeleteBytes()
        {
            return false;
        }
    }
}
