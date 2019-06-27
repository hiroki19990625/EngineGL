namespace EngineGL.Editor.Impl.Controls.Window
{
    partial class MainWindow
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solutionFilesSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoBuildAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noDebugStartNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015LightTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.buildBToolStripMenuItem,
            this.debugDToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.solutionFilesSToolStripMenuItem});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // solutionFilesSToolStripMenuItem
            // 
            this.solutionFilesSToolStripMenuItem.Name = "solutionFilesSToolStripMenuItem";
            this.solutionFilesSToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.solutionFilesSToolStripMenuItem.Text = "Open Solution Files(&S)";
            this.solutionFilesSToolStripMenuItem.Click += new System.EventHandler(this.SolutionFilesSToolStripMenuItem_Click);
            // 
            // buildBToolStripMenuItem
            // 
            this.buildBToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoBuildAToolStripMenuItem});
            this.buildBToolStripMenuItem.Name = "buildBToolStripMenuItem";
            this.buildBToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.buildBToolStripMenuItem.Text = "Build(&B)";
            // 
            // autoBuildAToolStripMenuItem
            // 
            this.autoBuildAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem,
            this.offToolStripMenuItem});
            this.autoBuildAToolStripMenuItem.Name = "autoBuildAToolStripMenuItem";
            this.autoBuildAToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.autoBuildAToolStripMenuItem.Text = "Auto Build(&A)";
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            this.onToolStripMenuItem.Size = new System.Drawing.Size(113, 26);
            this.onToolStripMenuItem.Text = "On";
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(113, 26);
            this.offToolStripMenuItem.Text = "Off";
            // 
            // debugDToolStripMenuItem
            // 
            this.debugDToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startSToolStripMenuItem,
            this.noDebugStartNToolStripMenuItem});
            this.debugDToolStripMenuItem.Name = "debugDToolStripMenuItem";
            this.debugDToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.debugDToolStripMenuItem.Text = "Debug(&D)";
            // 
            // startSToolStripMenuItem
            // 
            this.startSToolStripMenuItem.Name = "startSToolStripMenuItem";
            this.startSToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.startSToolStripMenuItem.Text = "Start(&S)";
            // 
            // noDebugStartNToolStripMenuItem
            // 
            this.noDebugStartNToolStripMenuItem.Name = "noDebugStartNToolStripMenuItem";
            this.noDebugStartNToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.noDebugStartNToolStripMenuItem.Text = "No Debug Start(&N)";
            this.noDebugStartNToolStripMenuItem.Click += new System.EventHandler(this.NoDebugStartNToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Location = new System.Drawing.Point(0, 28);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(800, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Location = new System.Drawing.Point(0, 426);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 24);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // dockPanel
            // 
            this.dockPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dockPanel.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel.Location = new System.Drawing.Point(0, 52);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Padding = new System.Windows.Forms.Padding(6);
            this.dockPanel.ShowAutoHideContentOnHover = false;
            this.dockPanel.Size = new System.Drawing.Size(800, 373);
            this.dockPanel.TabIndex = 3;
            this.dockPanel.Theme = this.vS2015LightTheme1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solutionFilesSToolStripMenuItem;
        private WeifenLuo.WinFormsUI.Docking.VS2015LightTheme vS2015LightTheme1;
        private System.Windows.Forms.ToolStripMenuItem buildBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoBuildAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noDebugStartNToolStripMenuItem;
    }
}