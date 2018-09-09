﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NFSU2.FileFormat;
using NFSU2.FileFormat.Sections;

namespace NFSU2
{
    public partial class FileViewer : Form
    {
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

                var mainSection = new Section(file, 0);

                tvFileStructure.Nodes.Add(mainSection);
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
                    writer.WriteLine($"o {part.Info.Name}");

                    foreach (var vertex in part.Vertices.Positions)
                    {
                        writer.WriteLine($"v {vertex.X} {vertex.Y} {vertex.Z}");
                    }

                    writer.WriteLine();

                    foreach (var normal in part.Vertices.Normals)
                    {
                        writer.WriteLine($"vn {normal.X} {normal.Y} {normal.Z}");
                    }

                    writer.WriteLine();

                    foreach (var texCoord in part.Vertices.TexCoordinates)
                    {
                        writer.WriteLine($"vt {texCoord.X} {texCoord.Y}");
                    }

                    writer.WriteLine();

                    for (var i = 0; i < part.Description.TriangleCount; i++)
                    {
                        writer.WriteLine($"f {part.Indices.Indices[i+0]+1} {part.Indices.Indices[i + 1]+1} {part.Indices.Indices[i + 2]+1}");
                    }

                    writer.Flush();
                    writer.Close();
                    outputStream.Close();
                }
            }
        }
    }
}
