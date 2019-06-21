using System;
using System.Collections.Generic;
using EngineGL.GraphicAdapter;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.Drawable.Shape2D
{
    public class SolidPolygonObject2D : DrawableObject
    {
        public List<Vec3> Points { get; } = new List<Vec3>();

        public SolidPolygonObject2D() : base(GraphicAdapterFactory.OpenGL2.CreatePolygon()) { }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);
            vertexHandler.SetVertces3(GetVec3s());
        }
        private IEnumerable<Vec3> GetVec3s()
        {
            Vec3 bou = new Vec3();
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].X < 0 && Points[i].Y < 0 && Points[i].Z < 0)
                    throw new ArgumentOutOfRangeException(nameof(Points),
                        Resources.Lang.Resources.SolidPolygonObject2D_OnDraw_Throw);

                yield return Points[i];
                bou.X = Math.Max(Points[i].X, bou.X);
                bou.Y = Math.Max(Points[i].Y, bou.Y);
                bou.Z = Math.Max(Points[i].Z, bou.Z);
            }

            Transform.Bounds = bou;
        }
    }
}