using EngineGL.Core.LifeCycle;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl
{
    /// <summary>
    /// シーンに追加されたIDrawableなオブジェクトの描画制御を実装します
    /// </summary>
    class DrawableList
    {
        private Dictionary<Guid, IDrawable> drawables = new Dictionary<Guid, IDrawable>(128);

        /// <summary>
        /// 新たなIDrawableを描画リストに追加します。
        /// guidが同じものを追加した場合は上書きされます
        /// </summary>
        /// <param name="guid">オブジェクトのguid</param>
        /// <param name="drawable">追加したいIDrawableオブジェクト</param>
        public void Add(Guid guid, IDrawable drawable)
        {
            drawables[guid] = drawable;
        }

        /// <summary>
        /// 描画リストからguidを指定してIDrawableを取り除きます。
        /// 存在しないguidの場合は無視されます
        /// </summary>
        /// <param name="guid">オブジェクトのguid</param>
        public void Remove(Guid guid)
        {
            drawables.Remove(guid);
        }

        public void OnDraw(double deltaTime)
        {
            foreach (IDrawable drawable in drawables.Values)
            {
                GL.PushAttrib(AttribMask.AllAttribBits);
                drawable.OnDraw(deltaTime);
                GL.PopAttrib();
            }
        }
    }
}