﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using EngineGL.Editor.Core.Control.Window;
using EngineGL.Editor.Impl.Controls.Dialog;
using Microsoft.Build.BuildEngine;
using Microsoft.Build.Construction;
using Microsoft.Build.Definition;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using WeifenLuo.WinFormsUI.Docking;
using Project = Microsoft.Build.Evaluation.Project;

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

        private SolutionFile _solution;
        private string _filePath;
        private Dictionary<string, Project> _projects = new Dictionary<string, Project>();

        public Dictionary<string, Action<string>> FileOpenWindowList = new Dictionary<string, Action<string>>();

        public SolutionTreeContent(IMainWindow hostWindow) : base(hostWindow)
        {
            InitializeComponent();

            FileOpenWindowList.Add(".cs", filePath =>
            {
                /*CodeParseTreeContent codeParseTree = new CodeParseTreeContent(hostWindow);
                codeParseTree.OpenFile(filePath);*/
            });

            Show(hostWindow.DockPanel, DockState.DockLeft);
        }

        public bool Build()
        {
            ToolStripProgressBar bar = new ToolStripProgressBar();
            bar.Maximum = _projects.Count;

            ToolStripLabel label = new ToolStripLabel();

            _hostWindow.StatusStrip.Items.Add(bar);
            _hostWindow.StatusStrip.Items.Add(label);
            foreach (KeyValuePair<string, Project> project in _projects)
            {
                ILogger logger = new ConsoleLogger();
                label.Text = "Building... " + project.Key;
                if (!project.Value.Build(logger))
                {
                    MessageBox.Show("Build Failed");
                    _hostWindow.StatusStrip.Items.Remove(bar);
                    _hostWindow.StatusStrip.Items.Remove(label);
                    bar.Dispose();
                    label.Dispose();
                    return false;
                }

                ++bar.Value;
            }

            _hostWindow.StatusStrip.Items.Remove(bar);
            _hostWindow.StatusStrip.Items.Remove(label);
            bar.Dispose();
            label.Dispose();

            return true;
        }

        public bool Exec()
        {
            int result = 0;
            foreach (KeyValuePair<string, Project> project in _projects)
            {
                Project proj = project.Value;
                ProjectProperty property = proj.GetProperty("OutputType");
                if (property.EvaluatedValue == "Exe")
                {
                    ProjectProperty output = proj.GetProperty("OutputPath");
                    ProjectProperty asm = proj.GetProperty("AssemblyName");
                    string exe = proj.DirectoryPath + Path.DirectorySeparatorChar + output.EvaluatedValue +
                                 asm.EvaluatedValue + ".exe";

                    Process.Start(new ProcessStartInfo(exe)
                    {
                        WorkingDirectory = Path.GetDirectoryName(exe)
                    });

                    result++;
                }
            }

            if (result == 0)
            {
                MessageBox.Show("Not Found Exe Project");
                return false;
            }

            return true;
        }

        public void LoadSolution(SolutionFile solution, string path)
        {
            _filePath = path;
            _solution = solution;

            string fileName = Path.GetFileName(path);

            treeView1.Nodes.Clear();
            _projects.Clear();
            ProjectCollection.GlobalProjectCollection.UnloadAllProjects();

            TreeNode node = treeView1.Nodes.Add(fileName);
            node.ImageIndex = SOLUTION;
            node.SelectedImageIndex = SOLUTION;

            foreach (ProjectInSolution projectSolution in solution.ProjectsInOrder)
            {
                Project project = Project.FromFile(projectSolution.AbsolutePath, new ProjectOptions());
                _projects.Add(projectSolution.ProjectName, project);

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

                if (item.ItemType == "Folder")
                {
                    string path = directory.FullName + "\\" +
                                  item.EvaluatedInclude.Remove(item.EvaluatedInclude.Length - 1, 1);
                    if (Directory.Exists(path))
                    {
                        if (!docs.Contains(path))
                            docs.Add(path);

                        string[] folders = path.Split('\\');
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
                        n.Tag = file.FullName;
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
            this.toolStripButton1.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "Tree Update";
            this.toolStripButton1.Click += new System.EventHandler(this.ToolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::EngineGL.Editor.Properties.Resources.ExpandAll;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "Expand All";
            this.toolStripButton2.Click += new System.EventHandler(this.ToolStripButton2_Click);
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
            this.toolStripButton3.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.Click += new System.EventHandler(this.ToolStripButton3_Click);
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
            this.toolStripButton4.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.Click += new System.EventHandler(this.ToolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::EngineGL.Editor.Properties.Resources.AddItem;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton5.Text = "toolStripButton5";
            this.toolStripButton5.Click += new System.EventHandler(this.ToolStripButton5_Click);
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
            this.treeView1.NodeMouseDoubleClick +=
                new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseDoubleClick);
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

        private void ToolStripButton1_Click(object sender, System.EventArgs e)
        {
            if (_solution != null && !string.IsNullOrEmpty(_filePath))
                LoadSolution(_solution, _filePath);
        }

        private void ToolStripButton2_Click(object sender, System.EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            string filePath = node.Tag?.ToString();
            if (filePath == null)
                return;
            ;
            string ext = Path.GetExtension(filePath);

            if (FileOpenWindowList.ContainsKey(ext))
            {
                FileOpenWindowList[ext].Invoke(filePath);
            }
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node.ImageIndex == PROJECT || node.ImageIndex == FOLDER || node.ImageIndex == FOLDER_OPEN)
            {
                string path =
                    _filePath.Replace(Path.DirectorySeparatorChar + Path.GetFileName(_filePath) ?? string.Empty, "");
                string replace = node.FullPath?.Replace(Path.GetFileName(_filePath) ?? string.Empty, "");
                string target = path + replace.Replace("\\", Path.DirectorySeparatorChar.ToString());
                if (Directory.Exists(target))
                {
                    StringDialog dialog = new StringDialog((text) => !string.IsNullOrEmpty(text));
                    dialog.Description = "Create Directory Name";
                    dialog.ShowDialog();
                    if (dialog.DialogResult == DialogResult.OK)
                    {
                        string newDir = target + Path.DirectorySeparatorChar + dialog.ResultString;
                        if (!Directory.Exists(newDir))
                        {
                            Directory.CreateDirectory(newDir);
                            string name = replace.Split(Path.DirectorySeparatorChar)[1];
                            Project proj = _projects[name];
                            proj.AddItem("Folder", newDir.Replace(path + "\\" + name + "\\", "") + "\\");
                            proj.Save();

                            LoadSolution(_solution, _filePath);
                        }
                        else
                        {
                            MessageBox.Show("Created Directory");
                        }
                    }
                }
            }
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
        }
    }
}