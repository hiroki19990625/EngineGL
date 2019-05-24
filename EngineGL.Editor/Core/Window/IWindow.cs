using System;
using System.Windows.Forms;

namespace EngineGL.Editor.Core.Window
{
    public interface IWindow
    {
        Guid WindowId { get; }

        void WindowToolStrip(ToolStrip toolStrip);
    }
}