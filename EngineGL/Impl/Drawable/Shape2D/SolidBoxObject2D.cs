using EngineGL.Structs.Math;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Shape2D
{
    public class SolidBoxObject2D : DrawableObject
    {
        public Color4 BoxColor { get; set; }

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
            
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(BoxColor);
            GL.Vertex3(Transform.Position + new Vec3(-Transform.Bounds.X / 2, -Transform.Bounds.Y / 2));
            GL.Vertex3(Transform.Position + new Vec3(-Transform.Bounds.X / 2, Transform.Bounds.Y / 2));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X / 2, Transform.Bounds.Y / 2, 0));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X / 2, -Transform.Bounds.Y / 2, 0));
            GL.End();
            GL.PopMatrix();
        }
    }
}