using System;
using System.Collections.Generic;
using EngineGL.GraphicAdapter;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.DrawableComponents.Shape3D
{
    public class SolidPolygonObject3D : DrawableComponent
    {
        public List<Vec3> Points { get; } = new List<Vec3>();

        public SolidPolygonObject3D() : base(GraphicAdapterFactory.CreatePolygon())
        {
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);
            vertexHandler.SetVertces3(GetVec3s());
        }

        public override void OnGraphicSetting(double deltaTime, ISettingHandler settingHandler)
        {
            //オイラー回転
            //Translateを使ってオブジェクトの原点に平行移動してから回転し、再び平行移動で元の位置に戻す
            settingHandler.Translate(GameObject.Transform.Position + GameObject.Transform.Bounds / 2);
            settingHandler.Euler(GameObject.Transform.LocalRotation);
            settingHandler.Translate((GameObject.Transform.Position + GameObject.Transform.Bounds / 2) * -1);
            settingHandler.Translate(GameObject.Transform.Position);

            //カラーセット
            settingHandler.SetColour(Colour);
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
                yield return Points[i] + new Vec3(0, 0, GameObject.Transform.Bounds.Z);
                bou.X = Math.Max(Points[i].X, bou.X);
                bou.Y = Math.Max(Points[i].Y, bou.Y);
                bou.Z = Math.Max(Points[i].Z, bou.Z);
            }

            for (int i = 0; i < Points.Count; i++)
            {
                foreach (Vec3 vec3 in (i == Points.Count - 1) ? SetSide(i, 0) : SetSide(i, i + 1))
                    yield return vec3;
            }

            bou.Z = Math.Max(bou.Z, GameObject.Transform.Bounds.Z);

            GameObject.Transform.Bounds = bou;
        }

        private IEnumerable<Vec3> SetSide(int now, int next)
        {
            yield return Points[now];
            yield return Points[next];
            yield return Points[next] + new Vec3(0, 0, GameObject.Transform.Bounds.Z);
            yield return Points[now] + new Vec3(0, 0, GameObject.Transform.Bounds.Z);
        }
    }
}