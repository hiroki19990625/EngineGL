using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable
{
    public class PointsObject : DrawableObject
    {
        public Color4 PointColor { get; set; }
        public float PointSize { get; set; } = 1;

        public override void OnDraw(double deltaTime)
        {
            base.OnDraw(deltaTime);

            GL.PointSize(PointSize);

            GL.Begin(PrimitiveType.Points);
            GL.Color4(PointColor);
            GL.Vertex3(Position);
            GL.End();
        }
    }
}