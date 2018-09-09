namespace NFSU2
{
    partial class FileViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.tvFileStructure = new System.Windows.Forms.TreeView();
            this.hexEditor = new Be.Windows.Forms.HexBox();
            this.btnInt32 = new System.Windows.Forms.Button();
            this.btnInt16 = new System.Windows.Forms.Button();
            this.btnFloat = new System.Windows.Forms.Button();
            this.btnDouble = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnMeshData = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(13, 13);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(330, 40);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // tvFileStructure
            // 
            this.tvFileStructure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvFileStructure.Location = new System.Drawing.Point(12, 59);
            this.tvFileStructure.Name = "tvFileStructure";
            this.tvFileStructure.Size = new System.Drawing.Size(331, 707);
            this.tvFileStructure.TabIndex = 1;
            this.tvFileStructure.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFileStructure_AfterSelect);
            // 
            // hexEditor
            // 
            this.hexEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hexEditor.ColumnInfoVisible = true;
            this.hexEditor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hexEditor.LineInfoVisible = true;
            this.hexEditor.Location = new System.Drawing.Point(350, 13);
            this.hexEditor.Name = "hexEditor";
            this.hexEditor.ReadOnly = true;
            this.hexEditor.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexEditor.Size = new System.Drawing.Size(1026, 753);
            this.hexEditor.StringViewVisible = true;
            this.hexEditor.TabIndex = 2;
            this.hexEditor.UseFixedBytesPerLine = true;
            this.hexEditor.VScrollBarVisible = true;
            // 
            // btnInt32
            // 
            this.btnInt32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInt32.Location = new System.Drawing.Point(1383, 13);
            this.btnInt32.Name = "btnInt32";
            this.btnInt32.Size = new System.Drawing.Size(168, 40);
            this.btnInt32.TabIndex = 3;
            this.btnInt32.Text = "32bit Integer";
            this.btnInt32.UseVisualStyleBackColor = true;
            this.btnInt32.Click += new System.EventHandler(this.btnInt32_Click);
            // 
            // btnInt16
            // 
            this.btnInt16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInt16.Location = new System.Drawing.Point(1382, 59);
            this.btnInt16.Name = "btnInt16";
            this.btnInt16.Size = new System.Drawing.Size(168, 40);
            this.btnInt16.TabIndex = 4;
            this.btnInt16.Text = "16bit Integer";
            this.btnInt16.UseVisualStyleBackColor = true;
            this.btnInt16.Click += new System.EventHandler(this.btnInt16_Click);
            // 
            // btnFloat
            // 
            this.btnFloat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFloat.Location = new System.Drawing.Point(1382, 105);
            this.btnFloat.Name = "btnFloat";
            this.btnFloat.Size = new System.Drawing.Size(168, 40);
            this.btnFloat.TabIndex = 5;
            this.btnFloat.Text = "Float";
            this.btnFloat.UseVisualStyleBackColor = true;
            this.btnFloat.Click += new System.EventHandler(this.btnFloat_Click);
            // 
            // btnDouble
            // 
            this.btnDouble.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDouble.Location = new System.Drawing.Point(1382, 151);
            this.btnDouble.Name = "btnDouble";
            this.btnDouble.Size = new System.Drawing.Size(168, 40);
            this.btnDouble.TabIndex = 6;
            this.btnDouble.Text = "Double";
            this.btnDouble.UseVisualStyleBackColor = true;
            this.btnDouble.Click += new System.EventHandler(this.btnDouble_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(1383, 198);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(63, 20);
            this.lblResult.TabIndex = 7;
            this.lblResult.Text = "Result: ";
            // 
            // btnMeshData
            // 
            this.btnMeshData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMeshData.Location = new System.Drawing.Point(1383, 726);
            this.btnMeshData.Name = "btnMeshData";
            this.btnMeshData.Size = new System.Drawing.Size(168, 40);
            this.btnMeshData.TabIndex = 8;
            this.btnMeshData.Text = "View MeshData";
            this.btnMeshData.UseVisualStyleBackColor = true;
            this.btnMeshData.Click += new System.EventHandler(this.btnMeshData_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(1383, 680);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(168, 40);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "Export SubMesh";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // FileViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1563, 778);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnMeshData);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnDouble);
            this.Controls.Add(this.btnFloat);
            this.Controls.Add(this.btnInt16);
            this.Controls.Add(this.btnInt32);
            this.Controls.Add(this.hexEditor);
            this.Controls.Add(this.tvFileStructure);
            this.Controls.Add(this.btnOpenFile);
            this.Name = "FileViewer";
            this.Text = "NFSU2 File Parser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TreeView tvFileStructure;
        private Be.Windows.Forms.HexBox hexEditor;
        private System.Windows.Forms.Button btnInt32;
        private System.Windows.Forms.Button btnInt16;
        private System.Windows.Forms.Button btnFloat;
        private System.Windows.Forms.Button btnDouble;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnMeshData;
        private System.Windows.Forms.Button btnExport;
    }
}

