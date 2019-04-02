using System;
using EngineGL.Core.LifeCycle;

namespace EngineGL.Core
{
    public interface IObject : Initialzeable, IDestroyable, IUpdateable, INameable
    {
        IScene Scene { get; set; }
        string Tag { get; set; }
    }
}