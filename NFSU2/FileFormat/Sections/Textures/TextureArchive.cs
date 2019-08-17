using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFSU2.Compression;

namespace NFSU2.FileFormat.Sections.Textures
{
    [Section(Header = SectionHeaders.TextureArchive)]
    public class TextureArchive : ISection
    {
        public ArchiveDetails Details { get; private set; }
        public FileList List { get; private set; }
        public FileDictionary LookupTable { get; private set; }

        private byte[] _data;

        public bool Parse(Section section)
        {
            var header = section.GetSection(SectionHeaders.ArchiveHeader);

            Details = header.GetSection(SectionHeaders.ArchiveDetails).Decode<ArchiveDetails>();
            List = header.GetSection(SectionHeaders.FileList).Decode<FileList>();
            LookupTable = header.GetSection(SectionHeaders.FileDictionary).Decode<FileDictionary>();

            _data = section.Data; 

            return true;
        }

        public byte[] GetFile(int fileHash)
        {
            var lookup = LookupTable.Files.First(f => f.Hash == fileHash);
            Console.WriteLine(fileHash.ToString("X4") + "@" + lookup.Offset.ToString("X4"));

            // we have to subtract 8 bytes here because the initial file header is not included in data
            var header = new[]
                {_data[lookup.Offset - 8], _data[lookup.Offset - 7], _data[lookup.Offset - 6], _data[lookup.Offset - 5]};

            //Console.WriteLine("Header: " + (char)header[0] + (char)header[1] + (char)header[2] + (char)header[3]);

            var compression = GetCompressionMethod(header);
            if (compression == null)
            {
                throw new UnknownCompressionMethod(header);
            }

            var input = new byte[lookup.CompressedLength];
            for (var i = lookup.Offset; i < lookup.Offset + lookup.CompressedLength; i++)
            {
                input[i - lookup.Offset] = _data[i - 8];
            }

            var decompressed = compression.Decompress(input);

            //Console.WriteLine($"Compressed size: {lookup.CompressedLength}  Decompressed size: {decompressed.Length} (expected {lookup.DecompressedLength})");

            return decompressed;
        }

        private ICompression GetCompressionMethod(byte[] header)
        {
            Debug.Assert(header.Length == 4);

            if (header[0] == 'H' && header[1] == 'U' && header[2] == 'F' && header[3] == 'F')
            {
                return new HUFFCompression();
            }

            if (header[0] == 'J' && header[1] == 'D' && header[2] == 'L' && header[3] == 'Z')
            {
                return new JDLZCompression();
            }

            return null;
        }
    }
}
