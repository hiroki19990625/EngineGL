using System.Windows.Forms;

namespace EngineGL.Utils
{
    /// <summary>
    /// マルチプラットフォームなダイアログをを提供します。
    /// </summary>
    public static class Dialog
    {
        public static DialogResult Show(string msg)
        {
            return Show(msg, string.Empty);
        }

        public static DialogResult Show(string msg, string title)
        {
            return Show(msg, title, MessageBoxButtons.OK);
        }

        public static DialogResult Show(string msg, string title, MessageBoxButtons buttons)
        {
            return Show(msg, title, buttons, MessageBoxIcon.None);
        }

        public static DialogResult Show(string msg, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show(msg, title, buttons, icon, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(string msg, string title, MessageBoxButtons buttons, MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton)
        {
            return Show(msg, title, buttons, icon, defaultButton, MessageBoxOptions.DefaultDesktopOnly);
        }

        public static DialogResult Show(string msg, string title, MessageBoxButtons buttons, MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            return Show(msg, title, buttons, icon, defaultButton, options, false);
        }

        public static DialogResult Show(string msg, string title, MessageBoxButtons buttons, MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool helpIcon)
        {
            return MessageBox.Show(msg, title, buttons, icon, defaultButton, options, helpIcon);
        }
    }
}