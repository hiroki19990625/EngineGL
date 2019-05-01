using System;
using OpenTK;

namespace EngineGL.Editor.Event
{
    public class GLControlEventArgs : EventArgs
    {
        public GLControl GLControl { get; }

        public GLControlEventArgs(GLControl control)
        {
            GLControl = control;
        }
    }
}