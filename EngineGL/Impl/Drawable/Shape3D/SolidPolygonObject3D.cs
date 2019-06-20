using System;
using System.Collections.Generic;
using EngineGL.GraphicAdapter;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.Drawable.Shape3D
{
    public class SolidPolygonObject3D : DrawableObject
    {
        public List<Vec3> Points { get; } = new List<Vec3>();
        public SolidPolygonObject3D() : base(GraphicAdapter.GraphicAdapterFactory.OpenGL2.CreatePolygon()) { }

        public override void OnPreprocessVertex(double deltaTime, IPreprocessVertexHandler preprocessVertexHandler)
        {
            base.OnPreprocessVertex(deltaTime, preprocessVertexHandler);
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);
            vertexHandler.Vertces3(GetVec3s());
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
            for (int i = 0; i < Points.Count; i++)
            {
                yield return Points[i] + new Vec3(0, 0, Transform.Bounds.Z);
                bou.X = Math.Max(Points[i].X, bou.X);
                bou.Y = Math.Max(Points[i].Y, bou.Y);
                bou.Z = Math.Max(Points[i].Z, bou.Z);
            }
            for (int i = 0; i < Points.Count; i++)
            {
                foreach (Vec3 vec3 in (i == Points.Count - 1) ? SetSide(i, 0) : SetSide(i, i + 1))
                    yield return vec3;
            }
            bou.Z = Math.Max(bou.Z, Transform.Bounds.Z);

            Transform.Bounds = bou;
        }

        private IEnumerable<Vec3> SetSide(int now, int next)
        {
            yield return Points[now];
            yield return Points[next];
            yield return Points[next] + new Vec3(0, 0, Transform.Bounds.Z);
            yield return Points[now] + new Vec3(0, 0, Transform.Bounds.Z);
        }
    }
}