using System;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Shape2D

{
    public class CircleObject2D : DrawableObject
    {
        public float Radius { get; set; }
        public Color4 CircleColor { get; set; }

        public override void OnDraw(double deltaTime)
        {
            base.OnDraw(deltaTime);
            GL.Begin(PrimitiveType.Lines);
            GL.Color4(CircleColor);
            for (float th1 = 0.0f; th1 <= 360.0f; th1 += 1.0f)
            {
                float th2 = th1 + 10.0f;
                float th1_rad = th1 / 180.0f * (float) Math.PI;
                float th2_rad = th2 / 180.0f * (float) Math.PI;

                float x1 = Radius * (float) Math.Cos(th1_rad) * Transform.Bounds.X;
                float y1 = Radius * (float) Math.Sin(th1_rad) * Transform.Bounds.Y;
                float x2 = Radius * (float) Math.Cos(th2_rad) * Transform.Bounds.X;
                float y2 = Radius * (float) Math.Sin(th2_rad) * Transform.Bounds.Y;

                GL.Vertex2(x1 + Transform.Position.X, y1 + Transform.Position.Y);
                GL.Vertex2(x2 + Transform.Position.X, y2 + Transform.Position.Y);
            }

            GL.End();
        }
    }
}