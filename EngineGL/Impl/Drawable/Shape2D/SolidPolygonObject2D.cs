using System;
using System.Collections.Generic;
using EngineGL.GraphicAdapter;
using EngineGL.Structs.Math;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Shape2D
{
    public class SolidPolygonObject2D : DrawableObject
    {
        public List<Vec3> Points { get; } = new List<Vec3>();
        public Color4 PoligonColor { get; set; }

        public SolidPolygonObject2D() : base(GraphicAdapterFactory.OpenGL1.CreatePolygon()) { }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);

            GL.Color4(PoligonColor);

            Vec3 bou = new Vec3();
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].X < 0 && Points[i].Y < 0 && Points[i].Z < 0)
                    throw new ArgumentOutOfRangeException(nameof(Points),
                        Resources.Lang.Resources.SolidPolygonObject2D_OnDraw_Throw);

                GL.Vertex3(Transform.Position + Points[i]);
                bou.X = Math.Max(Points[i].X, bou.X);
                bou.Y = Math.Max(Points[i].Y, bou.Y);
                bou.Z = Math.Max(Points[i].Z, bou.Z);
            }

            Transform.Bounds = bou;

        }
    }
}