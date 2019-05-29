using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EngineGL.Editor.Core.Control.Window;
using EngineGL.Editor.Impl.CodeBuilder;
using EngineGL.Editor.Impl.TreeWalker;
using Microsoft.CodeAnalysis.CSharp;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Impl.Controls.Window
{
    class CodeParseTreeContent : MyDockContent
    {
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private ImageList imageList1;
        private System.ComponentModel.IContainer components;
        private CSharpCodeDomBuilder _parser;

        public CodeParseTreeContent(IMainWindow hostWindow) : base(hostWindow)
        {
            InitializeComponent();

            Show(hostWindow.DockPanel, DockState.DockRight);
        }

        public void OpenFile(string filePath)
        {
            string str = File.ReadAllText(filePath, Encoding.UTF8);
            TreeNode root = this.treeView1.Nodes.Add(Path.GetFileName(filePath));
            CSharpSyntaxTree syntax = (CSharpSyntaxTree) CSharpSyntaxTree.ParseText(str);
            CSharpCodeWalker walker = new CSharpCodeWalker(root);
            walker.VisitCompilationUnit(syntax.GetCompilationUnitRoot());
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeParseTreeContent));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(282, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 25);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(282, 228);
            this.treeView1.TabIndex = 2;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "CS.png");
            this.imageList1.Images.SetKeyName(1, "Class.png");
            this.imageList1.Images.SetKeyName(2, "Interface.png");
            this.imageList1.Images.SetKeyName(3, "Structure.png");
            // 
            // CodeParseTreeContent
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CodeParseTreeContent";
            this.Text = "Code Parse Tree";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}