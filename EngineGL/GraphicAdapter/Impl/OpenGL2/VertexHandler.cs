
using EngineGL.Structs.Math;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using EngineGL.Structs.Drawing;

namespace EngineGL.GraphicAdapter.Impl.OpenGL2
{
    class VertexHandler : IVertexHandler
    {
        private int _vbo;
        private int _idxbo;
        private int _count = 0;
        private int _idxCount = 0;
        private int _groupCount;
        private PrimitiveType _primitiveType;
        private Color4 _color4 = Color4.White;

        public VertexHandler(PrimitiveType primitiveType, int groupCount)
        {
            _primitiveType = primitiveType;
            _groupCount = groupCount;
            //頂点データバッファー生成
            _vbo = GL.GenBuffer();
            //インデックスバッファー生成
            _idxbo = GL.GenBuffer();
        }

        public void Draw()
        {
            //インデックスバッファーのバインド
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _idxbo);
            //頂点データバッファーのバインド
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.EnableClientState(ArrayCap.VertexArray);
            //vertexの設定
            GL.VertexPointer(_groupCount, VertexPointerType.Float, 0, 0);

            // 描画
            GL.Color4(_color4);
            if (_idxCount == 0)
                GL.DrawArrays(_primitiveType, 0, _count);
            else
                GL.DrawElements(_primitiveType, _idxCount, DrawElementsType.UnsignedInt, 0);

            GL.DisableClientState(ArrayCap.VertexArray);
            //頂点データバッファーのバインドを解除
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //インデックスバッファーのバインドを解除
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Indices(IEnumerable<uint> indices)
        {
            uint[] indicesArray = indices.ToArray();
            //インデックスバッファー設定
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _idxbo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(uint) * indicesArray.Length), indicesArray, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            _idxCount = indicesArray.Length;
        }

        public void Vertces3(IEnumerable<Vec3> vecs)
        {
            Vector3[] vecArray = vecs.Select<Vec3, Vector3>(x => x).ToArray();
            //頂点データバッファー設定
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            int size = System.Runtime.InteropServices.Marshal.SizeOf(default(Vector3));
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(size * vecArray.Length), vecArray, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            _count = vecArray.Length;
        }

        public void Vertces2(IEnumerable<Vec2> vecs)
        {
            Vector2[] vecArray = vecs.Select<Vec2, Vector2>(x => x).ToArray();
            //頂点データバッファー設定
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            int size = System.Runtime.InteropServices.Marshal.SizeOf(default(Vector2));
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(size * vecArray.Length), vecArray, BufferUsageHint.StaticDraw);
            _count = vecArray.Length;
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void SetColour4(Colour4 colour4)
        {
            _color4 = colour4;
        }
    }
}
