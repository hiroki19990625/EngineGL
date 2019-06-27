using EngineGL.Editor.Core.Control.Window;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Impl.Controls.Window
{
    public class GamePreviewContent : MyDockContent
    {
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.IContainer components;
        private OpenTK.GLControl glControl1;

        public GamePreviewContent(IMainWindow hostWindow) : base(hostWindow)
        {
            InitializeComponent();

            Show(hostWindow.DockPanel, DockState.DockLeft);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.glControl1 = new OpenTK.GLControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl1.Location = new System.Drawing.Point(0, 0);
            this.glControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(282, 253);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            // 
            // GamePreviewContent
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.glControl1);
            this.Name = "GamePreviewContent";
            this.Text = "GamePreview";
            this.ResumeLayout(false);
        }
    }
}