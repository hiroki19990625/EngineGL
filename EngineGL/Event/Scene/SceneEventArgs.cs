using System;
using EngineGL.Core;

namespace EngineGL.Event.Scene
{
    public class SceneEventArgs : EventArgs
    {
        public IScene Scene { get; }

        public SceneEventArgs(IScene scene)
        {
            Scene = scene;
        }
    }
}