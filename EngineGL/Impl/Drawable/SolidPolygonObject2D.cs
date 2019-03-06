using System.Collections.Generic;
using EngineGL.Structs;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable
{
    public class SolidPolygonObject2D : DrawableObject
    {
        public List<Vec3> Points { get; } = new List<Vec3>();
        public Color4 PoligonColor { get; set; }

        public override void OnDraw()
        {
            base.OnDraw();

            GL.Begin(PrimitiveType.Polygon);
            GL.Color4(PoligonColor);
            for (int i = 0; i < Points.Count; i++)
            {
                GL.Vertex3(Position + Points[i]);
            }

            GL.End();
        }
    }
}