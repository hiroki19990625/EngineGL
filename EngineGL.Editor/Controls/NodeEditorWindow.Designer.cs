namespace EngineGL.Editor.Controls
{
    partial class NodeEditorWindow
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
            this.nodesControl1 = new NodeEditor.NodesControl();
            this.SuspendLayout();
            // 
            // nodesControl1
            // 
            this.nodesControl1.Context = null;
            this.nodesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodesControl1.Location = new System.Drawing.Point(0, 0);
            this.nodesControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nodesControl1.Name = "nodesControl1";
            this.nodesControl1.Size = new System.Drawing.Size(282, 253);
            this.nodesControl1.TabIndex = 0;
            // 
            // NodeEditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.nodesControl1);
            this.Name = "NodeEditorWindow";
            this.Text = "ScriptEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private NodeEditor.NodesControl nodesControl1;
    }
}
