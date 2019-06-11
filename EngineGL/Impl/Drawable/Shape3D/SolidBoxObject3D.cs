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
        private int _count = 0;

        public override void OnDraw(double deltaTime)
        {
            base.OnDraw(deltaTime);

            GL.PushMatrix();
            GL.Rotate(MathHelper.DegreesToRadians(10f * _count), 0, 1, 0);
            _count++;
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

            GL.End();
            GL.PopMatrix();

            //GL.LoadIdentity();
        }
    }
}