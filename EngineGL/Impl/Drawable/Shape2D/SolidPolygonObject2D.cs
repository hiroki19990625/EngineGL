using System;
using System.Collections.Generic;
using EngineGL.Structs.Math;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Shape2D
{
    public class SolidPolygonObject2D : DrawableObject
    {
        public List<Vec3> Points { get; } = new List<Vec3>();
        public Color4 PoligonColor { get; set; }

        public override void OnDraw(double deltaTime)
        {
            base.OnDraw(deltaTime);

            GL.MatrixMode(MatrixMode.Modelview);
            //現在の行列情報を保存
            GL.PushMatrix();
            //オイラー回転(Translateを使ってオブジェクトの原点に平行移動してから回転し、再び平行移動で元の位置に戻す)
            GL.Translate(Transform.Position+Transform.Bounds/2);
            GL.Rotate( Transform.Rotation.Y, 0, 1, 0);
            GL.Rotate( Transform.Rotation.Z, 0, 0, 1);
            GL.Rotate( Transform.Rotation.X, 1, 0, 0);
            GL.Translate((Transform.Position + Transform.Bounds / 2) * -1);
            GL.Begin(PrimitiveType.Polygon);
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

            GL.End();
            GL.PopMatrix();
        }
    }
}