using System;
using EngineGL.Core.LifeCycle;

namespace EngineGL.Core
{
    public interface IComponent : Initialzeable, IUpdateable, IDestroyable
    {
        IComponentAttachable ParentObject { get; set; }
    }
}