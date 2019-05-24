namespace EngineGL.Editor.Controls.Window
{
    partial class GameWindow
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.glControl = new OpenTK.GLControl();
            //
            // glControl
            //
            this.glControl.Name = "glControl";
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Load += new System.EventHandler(this.glControl_Load);
            this.glControl.Resize += new System.EventHandler(this.glControl_Resize);
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "GameWindow";
            this.Text = "Game";
            this.Closed += new System.EventHandler(this.gameWindow_Closed);
            this.Controls.Add(this.glControl);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl glControl;
    }
}
