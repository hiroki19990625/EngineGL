﻿
using EngineGL.Structs.Math;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using EngineGL.Structs.Drawing;
using EngineGL.Core.Resource;

namespace EngineGL.GraphicAdapter.Impl.OpenGL2
{
    /// <summary>
    /// OpenGL2によるIVertexHandlerの実装
    /// </summary>
    class VertexHandler : IVertexHandler
    {
        private static int Vector2Size = System.Runtime.InteropServices.Marshal.SizeOf(default(Vector2));
        private static int Vector3Size = System.Runtime.InteropServices.Marshal.SizeOf(default(Vector3));
        private int _vbo = 0;
        private int _idxbo = 0;
        private int _uvbo = 0;
        private int _vertexCount = 0;
        private int _idxCount = 0;
        private int _groupCount;
        private PrimitiveType _primitiveType;
        private Color4 _color4 = Color4.White;
        private ITexture _texture;

        public VertexHandler(PrimitiveType primitiveType, int groupCount)
        {
            _primitiveType = primitiveType;
            _groupCount = groupCount;
        }

        public void Draw()
        {
            //インデックスバッファーのバインド
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _idxbo);
            //頂点データバッファーのバインド
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            //vertexの設定
            GL.VertexPointer(_groupCount, VertexPointerType.Float, 0, 0);
            //uvバッファーのバインド
            GL.BindBuffer(BufferTarget.ArrayBuffer, _uvbo);
            //uvの設定
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, 0);

            //設定の有効化
            if (_vbo != 0) GL.EnableClientState(ArrayCap.VertexArray);
            if (_uvbo != 0)
            {
                GL.Enable(EnableCap.Texture2D);
                GL.EnableClientState(ArrayCap.TextureCoordArray);
            }

            // 描画
            GL.Color4(_color4);
            if (_texture != null) GL.BindTexture(TextureTarget.Texture2D, _texture.TextureHash);
            if (_idxCount == 0)
                GL.DrawArrays(_primitiveType, 0, _vertexCount);
            else
                GL.DrawElements(_primitiveType, _idxCount, DrawElementsType.UnsignedInt, 0);

            //設定の無効化
            if (_uvbo != 0)
            {
                GL.DisableClientState(ArrayCap.TextureCoordArray);
                GL.Disable(EnableCap.Texture2D);
            }
            if (_vbo != 0) GL.DisableClientState(ArrayCap.VertexArray);

            //頂点データ&UVバッファーのバインドを解除
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //インデックスバッファーのバインドを解除
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void SetIndices(IEnumerable<uint> indices)
        {
            uint[] indicesArray = indices.ToArray();

            //インデックスバッファー生成
            if (_idxbo == 0)
                _idxbo = GL.GenBuffer();

            //インデックスバッファー設定
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _idxbo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * indicesArray.Length, indicesArray, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            _idxCount = indicesArray.Length;
        }

        public void SetVertces3(IEnumerable<Vec3> vecs)
        {
            Vector3[] vecArray = vecs.Select<Vec3, Vector3>(x => x).ToArray();

            //頂点データバッファー生成
            if (_vbo == 0)
                _vbo = GL.GenBuffer();

            //頂点データバッファー設定
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Vector3Size * vecArray.Length, vecArray, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            _vertexCount = vecArray.Length;
        }

        public void SetVertces2(IEnumerable<Vec2> vecs)
        {
            Vector2[] vecArray = vecs.Select<Vec2, Vector2>(x => x).ToArray();

            //頂点データバッファー生成
            if (_vbo == 0)
                _vbo = GL.GenBuffer();


            //頂点データバッファー設定
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Vector2Size * vecArray.Length, vecArray, BufferUsageHint.StaticDraw);
            _vertexCount = vecArray.Length;
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void SetUv(IEnumerable<Vec2> vecs)
        {
            Vector2[] vecArray = vecs.Select<Vec2, Vector2>(x => x).ToArray();

            //UVバッファー生成
            if (_uvbo == 0)
                _uvbo = GL.GenBuffer();

            //UVバッファー設定
            GL.BindBuffer(BufferTarget.ArrayBuffer, _uvbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Vector2Size * vecArray.Length, vecArray, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        }

        public void SetTexture(ITexture texture)
        {
            _texture = texture;
        }

        public void SetColour4(Colour4 colour4)
        {
            _color4 = colour4;
        }
    }
}
