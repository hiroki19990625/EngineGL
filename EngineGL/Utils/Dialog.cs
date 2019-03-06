using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EngineGL.Utils
{
    public static class Dialog
    {
        public enum DialogType : uint
        {
            OK = 0x01,
            OK_CANCEL = 0x02,
            ABORT_RETRY_IGNORE = 0x03,
            YES_NO_CANCEL = 0x04,
            RETRY_CANCEL = 0x05,
            CANCEL_TRYCONTINUE = 0x06,
            ICON_ERROR = 0x10,
            ICON_QUESTION = 0x20,
            ICON_WARNING = 0x30,
            ICON_INFOMATION = 0x40,
            HELP = 0x4000
        }

        public enum DialogResult : int
        {
            OK = 1,
            CANCEL = 2,
            ABORT = 3,
            RETRY = 4,
            IGNORE = 5,
            YES = 6,
            NO = 7,
            TRYAGAIN = 10,
            CONTINUE = 11,
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr handle, string text, string caption, uint type);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult Open(string text)
        {
            return (DialogResult) Open("", text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult Open(string caption, string text)
        {
            return (DialogResult) Open(caption, text, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult Open(string caption, string text, DialogType type)
        {
            return (DialogResult) MessageBox(IntPtr.Zero, text, caption, (uint) type);
        }
    }
}