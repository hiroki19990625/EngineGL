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

        private SortedList<uint, Dictionary<Guid, IDrawable>> drawables
            = new SortedList<uint, Dictionary<Guid, IDrawable>>();

        /// <summary>
        /// 新たなIDrawableを描画リストに追加します。
        /// guidが同じものを追加した場合は上書きされます
        /// </summary>
        /// <param name="guid">オブジェクトのguid</param>
        /// <param name="drawable">追加したいIDrawableオブジェクト</param>
        public void Add(Guid guid, IDrawable drawable)
        {
            uint layer = uint.MaxValue - drawable.Layer;
            if (drawables.ContainsKey(layer) == false)
                drawables[layer] = new Dictionary<Guid, IDrawable>();
            drawables[layer][guid] = drawable;
        }

        /// <summary>
        /// 描画リストからguidを指定してIDrawableを取り除きます。
        /// 存在しないguidの場合は無視されます
        /// </summary>
        /// <param name="guid">オブジェクトのguid</param>
        public void Remove(Guid guid)
        {
            foreach (var drawables in drawables.Values)
                drawables.Remove(guid);
        }

        public void OnDraw(double deltaTime)
        {
            foreach (var drawables in drawables.Values)
            {
                foreach(var drawable in drawables.Values)
                {
                    GL.PushAttrib(AttribMask.AllAttribBits);
                    drawable.OnDraw(deltaTime);
                    GL.PopAttrib();
                }
            }
        }

    }
}