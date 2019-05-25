using System.Collections.Generic;
using System.Windows.Forms;
using EngineGL.Editor.Core.Control.Window;
using Microsoft.CodeAnalysis;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Impl.Controls.Window
{
    public class SolutionTreeContent : MyDockContent
    {
        private ToolStrip toolStrip1;
        private TreeView treeView1;
        private List<Project> _projects = new List<Project>();

        public SolutionTreeContent(IMainWindow hostWindow) : base(hostWindow)
        {
            InitializeComponent();

            Show(hostWindow.DockPanel, DockState.DockLeft);
        }

        public void LoadSolution(Solution solution)
        {
            foreach (ProjectId projectId in solution.GetProjectDependencyGraph().GetTopologicallySortedProjects())
            {
                Project project = solution.GetProject(projectId);
                _projects.Add(project);
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
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 25);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(282, 228);
            this.treeView1.TabIndex = 1;
            // 
            // SolutionTreeContent
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SolutionTreeContent";
            this.Text = "Solution Tree";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}