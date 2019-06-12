using System;
using EngineGL.Structs.Math;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Shape3D
{
    public class SolidBoxObject3D : DrawableObject
    {
        public Color4 BoxColor { get; set; }

        public override void OnDraw(double deltaTime)
        {
            base.OnDraw(deltaTime);

            //モデルビュー行列として扱うことで、カメラの位置に依存しないようにする
            GL.MatrixMode(MatrixMode.Modelview);
            //現在の行列情報を保存
            GL.PushMatrix();
            //オイラー回転(Translateを使ってオブジェクトの原点に平行移動してから回転し、再び平行移動で元の位置に戻す)
            GL.Translate(Transform.Position+Transform.Bounds/2);
            GL.Rotate( Transform.Rotation.Y, 0, 1, 0);
            GL.Rotate( Transform.Rotation.Z, 0, 0, 1);
            GL.Rotate( Transform.Rotation.X, 1, 0, 0);
            GL.Translate((Transform.Position + Transform.Bounds / 2) * -1);
            //頂点情報書き込み開始
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(BoxColor);
            //正面
            GL.Vertex3(Transform.Position);
            GL.Vertex3(Transform.Position + new Vec3(0, Transform.Bounds.Y, 0));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, Transform.Bounds.Y, 0));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, 0, 0));
            //背面
            GL.Vertex3(Transform.Position + new Vec3(0, 0, Transform.Bounds.Z));
            GL.Vertex3(Transform.Position + new Vec3(0, Transform.Bounds.Y, Transform.Bounds.Z));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, Transform.Bounds.Y, Transform.Bounds.Z));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, 0, Transform.Bounds.Z));
            //左側面
            GL.Vertex3(Transform.Position);
            GL.Vertex3(Transform.Position + new Vec3(0, Transform.Bounds.Y, 0));
            GL.Vertex3(Transform.Position + new Vec3(0, Transform.Bounds.Y, Transform.Bounds.Z));
            GL.Vertex3(Transform.Position + new Vec3(0, 0, Transform.Bounds.Z));
            //右側面
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, Transform.Bounds.Y, 0));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, 0, 0));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, 0, Transform.Bounds.Z));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, Transform.Bounds.Y, Transform.Bounds.Z));
            //上側面
            GL.Vertex3(Transform.Position + new Vec3(0, Transform.Bounds.Y, 0));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, Transform.Bounds.Y, 0));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, Transform.Bounds.Y, Transform.Bounds.Z));
            GL.Vertex3(Transform.Position + new Vec3(0, Transform.Bounds.Y, Transform.Bounds.Z));
            //下側面
            GL.Vertex3(Transform.Position);
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, 0, 0));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, 0, Transform.Bounds.Z));
            GL.Vertex3(Transform.Position + new Vec3(0, 0, Transform.Bounds.Z));

            //頂点情報書き込み終了
            GL.End();
            //保存していた行列情報で現在の行列情報を元に戻す
            GL.PopMatrix();
        }
    }
}