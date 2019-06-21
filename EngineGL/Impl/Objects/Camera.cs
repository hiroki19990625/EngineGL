using System;
using EngineGL.Event.LifeCycle;
using EngineGL.GraphicAdapter;
using EngineGL.Impl.DrawableComponents;
using EngineGL.Serializations.Resulter;
using EngineGL.Structs.Drawing;
using Newtonsoft.Json;
using OpenTK;

namespace EngineGL.Impl.Objects
{
    public abstract class Camera : DrawableComponent
    {
        protected Matrix4 _lookAtMatrix;
        [SerializeIgnore, JsonIgnore] public Matrix4 LookAtMatrix => _lookAtMatrix;

        protected Camera() : base(GraphicAdapterFactory.OpenGL2.CreatePoints())
        {
        }
    }
}