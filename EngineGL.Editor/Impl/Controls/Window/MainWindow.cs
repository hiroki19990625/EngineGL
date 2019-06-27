using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EngineGL.Editor.Core.Control.Window;
using EngineGL.Utils;
using Microsoft.Build.Construction;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Impl.Controls.Window
{
    public partial class MainWindow : Form, IMainWindow
    {
        private ConcurrentDictionary<Guid, MyDockContent> _myDockContents =
            new ConcurrentDictionary<Guid, MyDockContent>();

        private Guid _solutionTreeGuid;

        public MenuStrip MenuStrip => menuStrip;
        public ToolStrip ToolStrip => toolStrip;
        public StatusStrip StatusStrip => statusStrip;
        public DockPanel DockPanel => dockPanel;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void AddWindow(MyDockContent content)
        {
            _myDockContents.TryAdd(content.WindowGuid, content);
        }

        public void RemoveWindow(MyDockContent content)
        {
            _myDockContents.TryRemove(content.WindowGuid, out MyDockContent c);
        }

        public Result<MyDockContent> GetWindow(Guid guid)
        {
            if (_myDockContents.TryGetValue(guid, out MyDockContent val))
            {
                return Result<MyDockContent>.Success(val);
            }

            return Result<MyDockContent>.Fail();
        }

        public Result<T> GetWindow<T>(Guid guid) where T : MyDockContent
        {
            Result<MyDockContent> result = GetWindow(guid);
            if (result.IsSuccess)
            {
                return Result<T>.Success((T) result.Value);
            }

            return Result<T>.Fail();
        }

        public bool ContainsWindow(Guid guid)
        {
            return _myDockContents.ContainsKey(guid);
        }

        private void SolutionFilesSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Solution File|*sln";

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                SolutionFile solution = SolutionFile.Parse(fileName);
                SolutionTreeContent solutionTree;
                if (ContainsWindow(_solutionTreeGuid))
                {
                    solutionTree = GetWindow<SolutionTreeContent>(_solutionTreeGuid).Value;
                    solutionTree.LoadSolution(solution, fileName);
                }
                else
                {
                    solutionTree = new SolutionTreeContent(this);
                    solutionTree.LoadSolution(solution, fileName);

                    _solutionTreeGuid = solutionTree.WindowGuid;

                    AddWindow(solutionTree);
                }
            }
        }

        private void NoDebugStartNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Result<SolutionTreeContent> content = GetWindow<SolutionTreeContent>(_solutionTreeGuid);
            if (content.IsSuccess)
            {
                if (!content.Value.Build() &&
                    MessageBox.Show("Old Exe Execute?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) ==
                    DialogResult.No)
                {
                    return;
                }

                content.Value.Exec();
            }
        }
    }
}