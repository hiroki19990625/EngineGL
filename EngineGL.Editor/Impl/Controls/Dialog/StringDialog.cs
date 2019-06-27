using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EngineGL.Editor.Impl.Controls.Dialog
{
    public partial class StringDialog : Form
    {
        private Func<string, bool> _func;
        private bool _result;

        public string Description
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public string ResultString { get; private set; }

        public StringDialog(Func<string, bool> func)
        {
            _func = func;
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _result = _func(textBox1.Text);
            if (_result)
            {
                ResultString = textBox1.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void StringDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_result)
                DialogResult = DialogResult.Cancel;
        }
    }
}