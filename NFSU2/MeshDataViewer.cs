using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NFSU2.FileFormat;

namespace NFSU2
{
    partial class MeshDataViewer : Form
    {
        private Section _section;

        public MeshDataViewer(Section section)
        {
            InitializeComponent();

            _section = section;
        }

        private void MeshDataViewer_Load(object sender, EventArgs e)
        {
            grid.Columns.Add("posX", "Position X");
            grid.Columns.Add("posY", "Position Y");
            grid.Columns.Add("posZ", "Position Z");
            grid.Columns.Add("norX", "Normal X");
            grid.Columns.Add("norY", "Normal Y");
            grid.Columns.Add("norZ", "Normal Z");
            grid.Columns.Add("?", "?");
            grid.Columns.Add("texX", "Texture X");
            grid.Columns.Add("texY", "Texture Y");

            const int offset = 116;
            var verticesCount = (_section.Length - offset) / 4 / 9;
            Console.WriteLine(verticesCount);

            var position = offset;

            for (var i = 0; i < verticesCount; i++)
            {
                var a = BitConverter.ToSingle(_section.Data, position);
                position += 4;

                var b = BitConverter.ToSingle(_section.Data, position);
                position += 4;

                var c = BitConverter.ToSingle(_section.Data, position);
                position += 4;

                var d = BitConverter.ToSingle(_section.Data, position);
                position += 4;

                var f = BitConverter.ToSingle(_section.Data, position);
                position += 4;

                var g = BitConverter.ToSingle(_section.Data, position);
                position += 4;

                var h = BitConverter.ToSingle(_section.Data, position);
                position += 4;

                var j = BitConverter.ToSingle(_section.Data, position);
                position += 4;

                var k = BitConverter.ToSingle(_section.Data, position);
                position += 4;

                grid.Rows.Add(a, b, c, d, f, g, h, j, k);
            }
        }
    }
}
