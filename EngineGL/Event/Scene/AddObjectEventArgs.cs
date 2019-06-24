using EngineGL.Core;

namespace EngineGL.Event.Scene
{
    public class AddObjectEventArgs : SceneEventArgs
    {
        public IObject AddObject { get; }

        public AddObjectEventArgs(IScene scene, IObject addObject) : base(scene)
        {
            AddObject = addObject;
        }
    }
}