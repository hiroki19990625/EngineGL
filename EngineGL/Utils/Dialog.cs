using System;
using System.Runtime.InteropServices;

namespace EngineGL.Utils
{
    public static class Dialog
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr handle, string message, string title, int type);
        
        public static int Open(string message)
        {
            return Open("", message);
        }

        public static int Open(string title, string message)
        {
            return Open(title, message, 0);
        }

        public static int Open(string title, string message, int type)
        {
            return MessageBox(IntPtr.Zero, message, title, type);
        }
    }
}