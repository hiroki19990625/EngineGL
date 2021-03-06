﻿using System;
using System.Collections.Generic;
using System.Linq;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Structs.Math;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.GraphicAdapter.Impl.OpenGL2
{
    /// <summary>
    /// OpenGL2によるIVertexHandlerの実装
    /// </summary>
    class VertexHandler : IVertexHandler, IDisposable
    {
        private int _vbo = 0;
        private int _idxbo = 0;
        private int _nbo = 0;
        private int _uvbo = 0;
        private int _vertexCount = 0;
        private int _idxCount = 0;
        private int _dimension = 3;
        private PrimitiveType _primitiveType;

        public VertexHandler(PrimitiveType primitiveType)
        {
            _primitiveType = primitiveType;
        }

        public void Draw()
        {
            //インデックスバッファーのバインド
            if (_idxbo != 0)
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _idxbo);

            //頂点データバッファーのバインド
            if (_vbo != 0)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);

                //vertexの設定
                GL.VertexPointer(_dimension, VertexPointerType.Float, 0, 0);
            }

            if (_nbo != 0)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, _nbo);

                GL.NormalPointer(NormalPointerType.Float, 0, 0);
            }

            if (_uvbo != 0)
            {
                //uvバッファーのバインド
                GL.BindBuffer(BufferTarget.ArrayBuffer, _uvbo);
                //uvの設定
                GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, 0);
            }

            //設定の有効化
            if (_vbo != 0) GL.EnableClientState(ArrayCap.VertexArray);
            if (_nbo != 0) GL.EnableClientState(ArrayCap.NormalArray);
            if (_uvbo != 0)
            {
                GL.Enable(EnableCap.Texture2D);
                GL.EnableClientState(ArrayCap.TextureCoordArray);
            }

            // 描画
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

            if (_nbo != 0)
                GL.DisableClientState(ArrayCap.NormalArray);

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
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * indicesArray.Length, indicesArray,
                BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            _idxCount = indicesArray.Length;
        }

        public void SetVertces3(IEnumerable<Vec3> vecs)
        {
            _dimension = 3;
            Vector3[] vecArray = vecs.Select<Vec3, Vector3>(x => x).ToArray();

            //頂点データバッファー生成
            if (_vbo == 0)
                _vbo = GL.GenBuffer();

            //頂点データバッファー設定
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Vector3.SizeInBytes * vecArray.Length, vecArray,
                BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            _vertexCount = vecArray.Length;
        }

        public void SetVertces2(IEnumerable<Vec2> vecs)
        {
            _dimension = 2;
            Vector2[] vecArray = vecs.Select<Vec2, Vector2>(x => x).ToArray();

            //頂点データバッファー生成
            if (_vbo == 0)
                _vbo = GL.GenBuffer();


            //頂点データバッファー設定
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Vector2.SizeInBytes * vecArray.Length, vecArray,
                BufferUsageHint.StaticDraw);
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
            GL.BufferData(BufferTarget.ArrayBuffer, Vector2.SizeInBytes * vecArray.Length, vecArray,
                BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void SetNormals(IEnumerable<Vec3> normals)
        {
            Vector3[] vecArray = normals.Select<Vec3, Vector3>(x => x).ToArray();

            if (_nbo == 0)
                _nbo = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _nbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Vector3.SizeInBytes * vecArray.Length, vecArray,
                BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        /// <summary>
        /// 終了処理です。
        /// 主に、bufferの解放処理を行います
        /// </summary>
        public void Dispose()
        {
            if (_vbo != 0)
                GL.DeleteBuffer(_vbo);
            if (_idxbo != 0)
                GL.DeleteBuffer(_idxbo);
            if (_nbo != 0)
                GL.DeleteBuffer(_nbo);
            if (_uvbo != 0)
                GL.DeleteBuffer(_uvbo);
        }
    }
}