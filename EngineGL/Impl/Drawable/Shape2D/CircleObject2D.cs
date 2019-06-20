using System;
using System.Linq;
using System.Collections.Generic;
using EngineGL.GraphicAdapter;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.Drawable.Shape2D

{
    public class CircleObject2D : DrawableObject
    {
        public float Radius { get; set; }

        public CircleObject2D() : base(GraphicAdapterFactory.OpenGL2.CreateLinesStrip()) { }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);
            vertexHandler.Vertces2(GetVec2s().ToArray());
        }

        private IEnumerable<Vec2> GetVec2s()
        {
            for (float th1 = 0.0f; th1 <= 360.0f; th1 += 1.0f)
            {
                float th1_rad = th1 / 180.0f * (float)Math.PI;

                float x1 = Radius * (float)Math.Cos(th1_rad) * Transform.Bounds.X;
                float y1 = Radius * (float)Math.Sin(th1_rad) * Transform.Bounds.Y;
                yield return new Vec2(x1, y1);
            }
        }
    }
}