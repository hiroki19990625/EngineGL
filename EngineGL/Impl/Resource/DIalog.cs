using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EngineGL.Impl.Resource
{
    public class Dialog
    {
        public Dialog()
        {

        }

        public void OpenDialog(string message)
        {
            MessageBox.Show(message);
        }
    }
}
