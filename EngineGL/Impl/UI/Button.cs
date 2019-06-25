using System;
using System.Drawing;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;
using EngineGL.GraphicAdapter;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Impl.DrawableComponents;
using EngineGL.Structs.Drawing;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.UI
{
    public class Button : DrawableComponent, IClickable
    {
        public string Text { get; set; }
        public Font Font { get; set; }
        public int FontSize { get; set; }
        public Color FontColor { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public event EventHandler<ClickEventArgs> Click;

        public Button() : base(GraphicAdapterFactory.OpenGL2.CreateQuads())
        {
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);
            vertexHandler.SetVertces3(new Vec3[]
            {
                new Vec3(-GameObject.Transform.Bounds.X / 2, -GameObject.Transform.Bounds.Y / 2),
                new Vec3(GameObject.Transform.Bounds.X / 2, -GameObject.Transform.Bounds.Y / 2),
                new Vec3(GameObject.Transform.Bounds.X / 2, GameObject.Transform.Bounds.Y / 2),
                new Vec3(-GameObject.Transform.Bounds.X / 2, GameObject.Transform.Bounds.Y / 2),
            });
        }

        public void OnClick()
        {
            Colour = Color.Blue;
        }
    }
}