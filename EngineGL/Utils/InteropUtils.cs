using System;
using System.Runtime.InteropServices;

namespace EngineGL.Utils
{
    /// <summary>
    /// 相互運用機能のプラットフォーム切り替えを提供します。
    /// </summary>
    public static class InteropUtils
    {
        internal static void SwitchPlatform()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                if (IntPtr.Size == 8)
                    Switch_x64();
                else if (IntPtr.Size == 4)
                    Switch_x86();
            }
        }

        private static void Switch_x86()
        {
            SetDllDirectory(null);

            SetDllDirectory(Environment.CurrentDirectory + "/Dlls/x86");
        }

        private static void Switch_x64()
        {
            SetDllDirectory(null);

            SetDllDirectory(Environment.CurrentDirectory + "/Dlls/x64");
        }

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool SetDllDirectory(string lpPathName);
    }
}