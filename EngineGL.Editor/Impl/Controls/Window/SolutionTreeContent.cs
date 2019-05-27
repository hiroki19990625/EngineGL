using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using EngineGL.Editor.Core.Control.Window;
using Microsoft.Build.Construction;
using Microsoft.Build.Definition;
using Microsoft.Build.Evaluation;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Impl.Controls.Window
{
    public class SolutionTreeContent : MyDockContent
    {
        public const int FOLDER = 0;
        public const int FOLDER_OPEN = 1;
        public const int SOLUTION = 2;
        public const int PROJECT = 3;
        public const int CS_CODE = 4;
        public const int CONFIG = 5;
        public const int PROPERTY = 6;
        public const int DLL = 7;
        public const int FILE = 8;

        private ToolStrip toolStrip1;
        private TreeView treeView1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ImageList imageList1;
        private System.ComponentModel.IContainer components;
        private List<Project> _projects = new List<Project>();

        public SolutionTreeContent(IMainWindow hostWindow) : base(hostWindow)
        {
            InitializeComponent();

            Show(hostWindow.DockPanel, DockState.DockLeft);
        }

        public void LoadSolution(SolutionFile solution, string path)
        {
            string fileName = Path.GetFileName(path);

            treeView1.Nodes.Clear();
            _projects.Clear();

            TreeNode node = treeView1.Nodes.Add(fileName);
            node.ImageIndex = SOLUTION;
            node.SelectedImageIndex = SOLUTION;

            foreach (ProjectInSolution projectSolution in solution.ProjectsInOrder)
            {
                Project project = Project.FromFile(projectSolution.AbsolutePath, new ProjectOptions());
                _projects.Add(project);

                LoadProject(project, projectSolution.ProjectName, node);
            }
        }

        private void LoadProject(Project project, string name, TreeNode root)
        {
            Queue<(DirectoryInfo dir, TreeNode node)> queue = new Queue<(DirectoryInfo dir, TreeNode node)>();
            DirectoryInfo directory = new FileInfo(project.FullPath).Directory;

            List<string> docs = new List<string>();
            List<string> docFolders = new List<string>();
            foreach (ProjectItem item in project.Items)
            {
                if (item.ItemType == "Compile" ||
                    item.ItemType == "EmbeddedResource" || 
                    item.ItemType == "Content" ||
                    item.ItemType == "None")
                {
                    string path = directory.FullName + "\\" + item.EvaluatedInclude;
                    if (File.Exists(path))
                    {
                        if (!docs.Contains(path))
                            docs.Add(path);

                        string[] folders = Path.GetDirectoryName(path).Split('\\');
                        foreach (string folder in folders)
                        {
                            if (!docFolders.Contains(folder))
                                docFolders.Add(folder);
                        }
                    }
                }
            }

            TreeNode node = root.Nodes.Add(name);
            node.ImageIndex = PROJECT;
            node.SelectedImageIndex = PROJECT;

            queue.Enqueue((directory, node));

            while (queue.Count > 0)
            {
                (DirectoryInfo dir, TreeNode node) data = queue.Dequeue();
                foreach (DirectoryInfo d in data.dir.GetDirectories())
                {
                    if (docFolders.Contains(d.Name))
                    {
                        TreeNode n;
                        if (d.Name.ToLower() == "properties")
                        {
                            n = data.node.Nodes.Insert(0, d.Name);
                            n.ImageIndex = PROPERTY;
                            n.SelectedImageIndex = PROPERTY;
                        }
                        else
                        {
                            n = data.node.Nodes.Add(d.Name);
                            n.ImageIndex = FOLDER;
                            n.SelectedImageIndex = FOLDER;
                        }

                        queue.Enqueue((d, n));
                    }
                }

                foreach (FileInfo file in data.dir.GetFiles())
                {
                    if (docs.Contains(file.FullName))
                    {
                        TreeNode n = data.node.Nodes.Add(file.Name);
                        int img = GetFileImage(file.Name);
                        n.ImageIndex = img;
                        n.SelectedImageIndex = img;
                    }
                }
            }
        }

        private int GetFileImage(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext)
            {
                case ".cs":
                    return CS_CODE;

                case ".config":
                    return CONFIG;

                case ".dll":
                    return DLL;

                default:
                    return FILE;
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(SolutionTreeContent));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.toolStripButton1,
                this.toolStripButton2,
                this.toolStripSeparator1,
                this.toolStripButton3,
                this.toolStripSeparator2,
                this.toolStripButton4,
                this.toolStripButton5
            });
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(282, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::EngineGL.Editor.Properties.Resources.Refresh;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::EngineGL.Editor.Properties.Resources.ExpandAll;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::EngineGL.Editor.Properties.Resources.NewSolutionFolder;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::EngineGL.Editor.Properties.Resources.NewItem;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::EngineGL.Editor.Properties.Resources.AddItem;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton5.Text = "toolStripButton5";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 27);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(282, 226);
            this.treeView1.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream =
                ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Folder.png");
            this.imageList1.Images.SetKeyName(1, "FolderOpen.png");
            this.imageList1.Images.SetKeyName(2, "CS_Application.png");
            this.imageList1.Images.SetKeyName(3, "CS_ProjectSENode.png");
            this.imageList1.Images.SetKeyName(4, "CS.png");
            this.imageList1.Images.SetKeyName(5, "ConfigurationFile.png");
            this.imageList1.Images.SetKeyName(6, "Property.png");
            this.imageList1.Images.SetKeyName(7, "Library.png");
            this.imageList1.Images.SetKeyName(8, "FileSource.png");
            // 
            // SolutionTreeContent
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SolutionTreeContent";
            this.Text = "Solution Tree";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}