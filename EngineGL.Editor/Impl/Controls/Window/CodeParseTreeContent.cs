using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EngineGL.Editor.Core.Control.Window;
using EngineGL.Editor.Impl.Parser;
using Microsoft.CodeAnalysis.CSharp;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Impl.Controls.Window
{
    class CodeParseTreeContent : MyDockContent
    {
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;

        private CSharpCodeParser _parser;

        public CodeParseTreeContent(IMainWindow hostWindow) : base(hostWindow)
        {
            InitializeComponent();

            Show(hostWindow.DockPanel, DockState.DockRight);
        }

        public void OpenFile(string filePath)
        {
            string str = File.ReadAllText(filePath, Encoding.UTF8);
            CSharpSyntaxTree syntax = (CSharpSyntaxTree) CSharpSyntaxTree.ParseText(str);

            _parser = new CSharpCodeParser(syntax.GetCompilationUnitRoot());
            CodeCompileUnit unit = _parser.Parse();
            if (unit == null)
                return;

            CodeNamespace unitNamespace = unit.Namespaces[0];
            TreeNode importNode = treeView1.Nodes.Add("Imports");
            foreach (CodeNamespaceImport import in unitNamespace.Imports)
            {
                importNode.Nodes.Add(import.Namespace);
            }

            CodeNamespace body = unitNamespace = unit.Namespaces[1];
            TreeNode cls = treeView1.Nodes.Add(body.Name);
            foreach (CodeTypeDeclaration declaration in body.Types)
            {
                TreeNode node = cls.Nodes.Add(declaration.Name);
                foreach (CodeTypeMember member in declaration.Members)
                {
                    node.Nodes.Add(member.Name);
                }
            }
        }

        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.treeView1 = new System.Windows.Forms.TreeView();
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
            this.treeView1.Location = new System.Drawing.Point(0, 25);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(282, 228);
            this.treeView1.TabIndex = 2;
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