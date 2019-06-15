using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using EngineGL.GraphicAdapter;
using EngineGL.Structs.Math;
using NLog.LayoutRenderers.Wrappers;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Shape3D
{
    public class SolidPolygonObject3D : DrawableObject
    {
        public List<Vec3> Points { get; } = new List<Vec3>();
        public Color4 PoligonColor { get; set; }

        public SolidPolygonObject3D():base(GraphicAdapter.GraphicAdapterFactory.OpenGL1.CreatePolygon()){}

        public override void OnPreprocessVertex(double deltaTime, IPreprocessVertexHandler preprocessVertexHandler)
        {
            base.OnPreprocessVertex(deltaTime, preprocessVertexHandler);
            //オイラー回転(Translateを使ってオブジェクトの原点に平行移動してから回転し、再び平行移動で元の位置に戻す)
            GL.Translate(Transform.Position + Transform.Bounds / 2);
            GL.Rotate(Transform.Rotation.Y, 0, 1, 0);
            GL.Rotate(Transform.Rotation.Z, 0, 0, 1);
            GL.Rotate(Transform.Rotation.X, 1, 0, 0);
            GL.Translate((Transform.Position + Transform.Bounds / 2) * -1);
        }

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
            for (int i = 0; i < Points.Count; i++)
            {

                GL.Vertex3(Transform.Position + Points[i]+new Vec3(0,0,Transform.Bounds.Z));
                bou.X = Math.Max(Points[i].X, bou.X);
                bou.Y = Math.Max(Points[i].Y, bou.Y);
                bou.Z = Math.Max(Points[i].Z, bou.Z);
            }
            for (int i = 0; i < Points.Count; i++)
            {
                if (i == Points.Count - 1)
                {
                    SetSide(i,0);
                }
                else
                {
                    SetSide(i,i+1);
                }
                
                
            }
            bou.Z = Math.Max(bou.Z, Transform.Bounds.Z);

            Transform.Bounds = bou;
        }

        private void SetSide(int now ,int next)
        {
            GL.Vertex3(Transform.Position + Points[now]);
            GL.Vertex3(Transform.Position + Points[next]);
            GL.Vertex3(Transform.Position + Points[next]+new Vec3(0,0,Transform.Bounds.Z));
            GL.Vertex3(Transform.Position + Points[now]+new Vec3(0,0,Transform.Bounds.Z));
        }
    }
}