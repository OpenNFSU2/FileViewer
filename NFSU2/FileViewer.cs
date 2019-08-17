using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NFSU2.FileFormat;
using NFSU2.FileFormat.Sections;
using NFSU2.FileFormat.Sections.Geometry;
using NFSU2.FileFormat.Sections.Textures;

namespace NFSU2
{
    public partial class FileViewer : Form
    {
        private Section _mainSection;

        public FileViewer()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine($"Open file {dialog.FileName}");
                Text = dialog.FileName;

                var file = File.Open(dialog.FileName, FileMode.Open, FileAccess.Read);

                _mainSection = new Section(file, 0);

                if (_mainSection.Header == (uint) SectionHeaders.TextureArchive)
                {
                    var textureArchive = _mainSection.Decode<TextureArchive>();
                    Console.WriteLine(textureArchive.Details.FileName);
                    Console.WriteLine(textureArchive.Details.SourceName);
                    Console.WriteLine(textureArchive.List.Files.Count);

                    var tmpPath = Path.Combine(Path.GetTempPath(), "NFS");
                    Directory.CreateDirectory(tmpPath);

                    foreach (var f in textureArchive.List.Files)
                    {
                        //var outputFile = Path.Combine(Path.GetTempPath(), "NFS", f.ToString("X4"));
                        var data = textureArchive.GetFile(f);
                        if (data.Length == 0) continue; // decompression failed

                        // check if pixel data is uncompressed (reading image format)
                        byte[] imageFormat =
                        {
                            data[data.Length - 12],
                            data[data.Length - 11],
                            data[data.Length - 10],
                            data[data.Length -  9]
                        };

                        var size = 144;
                        var block = 0;
                        if (imageFormat[0] == 'D' && imageFormat[1] == 'X' && imageFormat[2] == 'T')
                        {
                            // image is compressed dxt format
                            block = 31;
                        } else
                        {
                            block = 32;
                            continue; // save not implemted yet
                        }

                        // read file name
                        var fileName = "";
                        for (var i = 0; i < 24; i++)
                        {
                            var c = (char)data[data.Length - size + i];
                            if (c == '\0') break;
                            fileName += c;
                        }
                        Console.WriteLine(fileName);

                        // read image size
                        byte[] imageWidthBytes =
                        {
                            data[data.Length - size + 24 + block + 0],
                            data[data.Length - size + 24 + block + 1],
                        };
                        var imageWidth = (imageWidthBytes[0] << 8) | imageWidthBytes[1];
                        byte[] imageHeightBytes =
                        {
                            data[data.Length - size + 24 + block + 2],
                            data[data.Length - size + 24 + block + 3],
                        };
                        var imageHeight = (imageHeightBytes[0] << 8) | imageHeightBytes[1];
                        Console.WriteLine($"{imageWidth}x{imageHeight}");

                        // read pitch or linear size
                        byte[] pitchOrLinearSizeBytes =
                        {
                            data[data.Length - size + 24 + block - 4],
                            data[data.Length - size + 24 + block - 3],
                            data[data.Length - size + 24 + block - 2],
                            data[data.Length - size + 24 + block - 1],
                        };
                        var pitchOrLinearSize = (pitchOrLinearSizeBytes[0] << 24) | (pitchOrLinearSizeBytes[1] << 16) | (pitchOrLinearSizeBytes[2] << 8) | pitchOrLinearSizeBytes[3];
                        Console.WriteLine($"pitchOrLinearSize {pitchOrLinearSize}");

                        byte mipMapCount = data[data.Length - size + 24 + block + 4 + 7];
                        Console.WriteLine($"mip map count {(int)mipMapCount}");

                        // create dds texture file
                        var filePath = Path.Combine(tmpPath, fileName + "_" + f.ToString("X4") + ".dds");
                        var ddsFileHeader = new List<byte>
                        {
                            /* header */
                            0x44, 0x44, 0x53, 0x20,
                            /* dwSize */
                            0x7C, 0x00, 0x00, 0x00,
                            /* dwFlags */
                            0x07, 0x10, 0x0A, 0x00,
                            /* dwHeight */
                            imageHeightBytes[1], imageHeightBytes[0], 0x00, 0x00,
                            /* dwWidth */
                            imageWidthBytes[1], imageWidthBytes[0], 0x00, 0x00,
                            /* dwPitchOrLinearSize */
                            pitchOrLinearSizeBytes[0], pitchOrLinearSizeBytes[1], pitchOrLinearSizeBytes[2], pitchOrLinearSizeBytes[3],
                            /* dwDepth */
                            0x01, 0x00, 0x00, 0x00,
                            /* dwMipMapCount */
                            mipMapCount, 0x00, 0x00, 0x00,
                            /* dwReserved1 */
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00,
                            /* ddspf.dwSize */
                            0x20, 0x00, 0x00, 0x00,
                            /* ddspf.dwFlags */
                            0x04, 0x00, 0x00, 0x00,
                            /* ddspf.dwFourCC */
                            imageFormat[0], imageFormat[1], imageFormat[2], imageFormat[3],
                            /* ddspf.dwRGBBitCount */
                            0x00, 0x00, 0x00, 0x00,
                            /* ddspf.dwRBitMask */
                            0x00, 0x00, 0x00, 0x00,
                            /* ddspf.dwGBitMask */
                            0x00, 0x00, 0x00, 0x00,
                            /* ddspf.dwBBitMask */
                            0x00, 0x00, 0x00, 0x00,
                            /* ddspf.dwABitMask */
                            0x00, 0x00, 0x00, 0x00,
                            /* dwCaps */
                            0x08, 0x10, 0x40, 0x00,
                            /* dwCaps2 */
                            0x00, 0x00, 0x00, 0x00,
                            /* dwCaps3 */
                            0x00, 0x00, 0x00, 0x00,
                            /* dwCaps4 */
                            0x00, 0x00, 0x00, 0x00,
                            /* dwReserved2 */
                            0x00, 0x00, 0x00, 0x00
                        };
                        ddsFileHeader.AddRange(data.Take(data.Length - size));
                        File.WriteAllBytes(filePath, ddsFileHeader.ToArray());
                    }
                }

                tvFileStructure.Nodes.Add(_mainSection);
            }
        }

        private void tvFileStructure_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var section = tvFileStructure.SelectedNode as Section;
            hexEditor.ByteProvider = section;
            hexEditor.LineInfoOffset = section.Position;

            if (section.Header == (uint)SectionHeaders.MeshPart)
            {
                btnExport.Enabled = true;
            }
            else
            {
                btnExport.Enabled = false;
            }
        }

        private void ShowErrorLength(int length)
        {
            lblResult.Text = "Length != " + length;
        }

        private void ShowResult(object res)
        {
            lblResult.Text = "Result: " + res;
        }

        private void btnInt32_Click(object sender, EventArgs e)
        {
            if (hexEditor.SelectionLength != 4)
            {
                ShowErrorLength(4);
                return;
            }

            var j = 0;
            var b = new byte[4];
            for (var i = hexEditor.SelectionStart;
                i < hexEditor.SelectionStart + hexEditor.SelectionLength;
                i++)
            {
                b[j++] = hexEditor.ByteProvider.ReadByte(i);
            }
            
            ShowResult(BitConverter.ToInt32(b, 0));
        }

        private void btnInt16_Click(object sender, EventArgs e)
        {
            if (hexEditor.SelectionLength != 2)
            {
                ShowErrorLength(2);
                return;
            }

            var j = 0;
            var b = new byte[2];
            for (var i = hexEditor.SelectionStart;
                i < hexEditor.SelectionStart + hexEditor.SelectionLength;
                i++)
            {
                b[j++] = hexEditor.ByteProvider.ReadByte(i);
            }

            ShowResult(BitConverter.ToInt16(b, 0));
        }

        private void btnFloat_Click(object sender, EventArgs e)
        {
            if (hexEditor.SelectionLength != 4)
            {
                ShowErrorLength(4);
                return;
            }

            var j = 0;
            var b = new byte[4];
            for (var i = hexEditor.SelectionStart;
                i < hexEditor.SelectionStart + hexEditor.SelectionLength;
                i++)
            {
                b[j++] = hexEditor.ByteProvider.ReadByte(i);
            }

            ShowResult(BitConverter.ToSingle(b, 0));
        }

        private void btnDouble_Click(object sender, EventArgs e)
        {
            if (hexEditor.SelectionLength != 4)
            {
                ShowErrorLength(4);
                return;
            }

            var j = 0;
            var b = new byte[4];
            for (var i = hexEditor.SelectionStart;
                i < hexEditor.SelectionStart + hexEditor.SelectionLength;
                i++)
            {
                b[j++] = hexEditor.ByteProvider.ReadByte(i);
            }

            ShowResult(BitConverter.ToDouble(b, 0));
        }

        private void btnMeshData_Click(object sender, EventArgs e)
        {
            var viewer = new MeshDataViewer(tvFileStructure.SelectedNode as Section);
            viewer.Show();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var section = tvFileStructure.SelectedNode as Section;
            if (section == null) return;

            var decoded = section.Decode();

            if (decoded is MeshPart part)
            {
                Console.WriteLine("Name: " + part.Info.Name);
                Console.WriteLine("Triangle Count: " + part.Description.TriangleCount);
                Console.WriteLine("Vertices Count: " + part.Description.VerticesCount);

                Console.WriteLine("Vertices Count 2: " + part.Vertices.Positions.Count);

                var dialog = new SaveFileDialog();
                dialog.DefaultExt = "obj";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var outputStream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write);
                    var writer = new StreamWriter(outputStream);
                    
                    writer.WriteLine("# Generated by OpenNFSU2");
                    writer.WriteLine("# developed by KryptonDevelopment & TeamQuantum 2018");
                    writer.WriteLine();

                    part.WriteObjFile(writer);

                    writer.Flush();
                    writer.Close();
                    outputStream.Close();
                }
            }
        }

        private void btnExportAll_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.DefaultExt = "obj";
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var outputStream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write);
            var writer = new StreamWriter(outputStream);

            writer.WriteLine("# Generated by OpenNFSU2");
            writer.WriteLine("# developed by KryptonDevelopment & TeamQuantum 2018");
            writer.WriteLine();

            var index = 0;

            var sections = _mainSection.GetSections(SectionHeaders.MeshPart);
            var offset = 0u;
            foreach (var section in sections)
            {
                var part = section.Decode<MeshPart>();
                part.WriteObjFile(writer, offset, $" #{index}");

                offset += (uint)part.Vertices.Positions.Count;

                index++;
            }

            writer.Flush();
            writer.Close();
            outputStream.Close();
        }
    }
}
