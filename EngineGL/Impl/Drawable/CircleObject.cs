using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;

namespace EngineGL.Impl.Drawable

{
    public class CircleObject : DrawableObject
    {
        public float Radius { get; set; }
        public Color4 CircleColor { get; set; }

        public override void OnDraw(double deltaTime)
        {
            base.OnDraw(deltaTime);
            GL.Begin(PrimitiveType.Lines);
            GL.Color4(CircleColor);
            for (float th1 = 0.0f; th1 <= 360.0f; th1 = th1 + 1.0f)
            {
                float th2 = th1 + 10.0f;
                float th1_rad = th1 / 180.0f * (float) Math.PI;
                float th2_rad = th2 / 180.0f * (float) Math.PI;

                float x1 = Radius * (float) Math.Cos(th1_rad) * (Bounds.X / 100.0f);
                float y1 = Radius * (float) Math.Sin(th1_rad) * (Bounds.Y / 100.0f);
                float x2 = Radius * (float) Math.Cos(th2_rad) * (Bounds.X / 100.0f);
                float y2 = Radius * (float) Math.Sin(th2_rad) * (Bounds.Y / 100.0f);


                GL.Vertex2(x1 + Position.X, y1 + Position.Y);
                GL.Vertex2(x2 + Position.X, y2 + Position.Y);
            }

            GL.End();
        }
    }
}