using System;
using System.IO;
using System.Threading.Tasks;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.Scene;
using EngineGL.Serializations.Resulter;
using EngineGL.Utils;

namespace EngineGL.Core
{
    /// <summary>
    /// <see cref="IGame"/> で利用されるゲームのシーンを実装します。
    /// </summary>
    public interface IScene : INameable, IUpdateable, IDrawable, IGuiRenderable, ISerializeResultJson
    {
        /// <summary>
        /// オブジェクトが追加された時に発火するイベント。
        /// </summary>
        event EventHandler<AddObjectEventArgs> AddObjectEvent;

        /// <summary>
        /// オブジェクトが削除された時に発火するイベント。
        /// </summary>
        event EventHandler<RemoveObjectEventArgs> RemoveObjectEvent;

        /// <summary>
        /// このシーン上の全てのオブジェクトを取得します。
        /// </summary>
        /// <returns>
        /// シーン上の全てのオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IObject[]> GetObjects();

        /// <summary>
        /// インスタンスID からシーン上のオブジェクトを取得します。
        /// </summary>
        /// <param name="hash">インスタンスID</param>
        /// <returns>
        /// シーン上のオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IObject> GetObject(Guid hash);

        /// <summary>
        /// インスタンスID からシーン上のオブジェクトを取得します。
        /// </summary>
        /// <param name="hash">インスタンスID</param>
        /// <typeparam name="T"><see cref="IComponent"/>型の型引数</typeparam>
        /// <returns>
        /// シーン上のオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<T> GetObjectUnsafe<T>(Guid hash) where T : IScene;

        /// <summary>
        /// オブジェクトをシーンに追加します。
        /// </summary>
        /// <param name="obj">インスタンス</param>
        /// <returns>
        /// シーン上のオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IObject> AddObject(IObject obj);

        /// <summary>
        /// オブジェクトをシーンに追加します。
        /// </summary>
        /// <param name="obj">インスタンス</param>
        /// <typeparam name="T"><see cref="IComponent"/>型の型引数</typeparam>
        /// <returns>
        /// シーンに追加されたオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<T> AddObjectUnsafe<T>(T obj) where T : IObject;

        /// <summary>
        /// ジェネリクス型情報を基にインスタンスを作成してオブジェクトをシーンに追加します。
        /// </summary>
        /// <typeparam name="T"><see cref="IComponent"/>型の型引数</typeparam>
        /// <returns>
        /// シーンに追加されたオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<T> AddObjectUnsafe<T>() where T : IObject;

        /// <summary>
        /// 型情報を基にインスタンスを作成してオブジェクトをシーンに追加します。
        /// </summary>
        /// <param name="type"><see cref="IComponent"/>型の型引数</param>
        /// <returns>
        /// シーンに追加されたオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IObject> AddObjectUnsafe(Type type);

        /// <summary>
        /// 複数のオブジェクトをシーンに追加します。
        /// </summary>
        /// <param name="objs">オブジェクトのインスタンス</param>
        /// <returns>
        /// シーンに追加されたオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IObject[]> AddObjects(params IObject[] objs);

        /// <summary>
        /// 複数のオブジェクトをシーンに追加します。
        /// </summary>
        /// <param name="objs">オブジェクトのインスタンス</param>
        /// <typeparam name="T"><see cref="IComponent"/>型の型引数</typeparam>
        /// <returns>
        /// シーンに追加されたオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<T[]> AddObjectsUnsafe<T>(params T[] objs) where T : IObject;

        /// <summary>
        /// インスタンスID からシーンのオブジェクトを削除します。
        /// </summary>
        /// <param name="hash">オブジェクトのインスタンスID</param>
        /// <returns>
        /// シーンから削除されたオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IObject> RemoveObject(Guid hash);

        /// <summary>
        /// インスタンスからシーンのオブジェクトを削除します。
        /// </summary>
        /// <param name="obj">オブジェクトのインスタンス</param>
        /// <returns>
        /// シーンから削除されたオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IObject> RemoveObject(IObject obj);

        /// <summary>
        /// インスタンスからシーンのオブジェクトを削除します。
        /// </summary>
        /// <param name="obj">オブジェクトのインスタンス</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// シーンから削除されたオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<T> RemoveObjectUnsafe<T>(T obj) where T : IObject;

        /// <summary>
        /// シーン上のオブジェクトを複数削除します。
        /// </summary>
        /// <param name="hashs">オブジェクトのインスタンスID</param>
        /// <returns>
        /// シーンから削除されたオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IObject[]> RemoveObjects(params Guid[] hashs);

        /// <summary>
        /// シーン上のオブジェクトを複数削除します。
        /// </summary>
        /// <param name="objs">オブジェクトのインスタンス</param>
        /// <returns>
        /// シーンから削除されたオブジェクトを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IObject[]> RemoveObjects(params IObject[] objs);

        /// <summary>
        /// このシーンを保存します。
        /// </summary>
        /// <param name="filePath">ファイルのパス</param>
        void Save(string filePath);

        /// <summary>
        /// このシーンを保存します。
        /// </summary>
        /// <param name="file">ファイル情報</param>
        void Save(FileInfo file);

        /// <summary>
        /// このシーンを非同期で保存します。
        /// </summary>
        /// <param name="filePath">ファイルのパス</param>
        /// <returns></returns>
        Task SaveAsync(string filePath);

        /// <summary>
        /// このシーンを非同期で保存します。
        /// </summary>
        /// <param name="fileInfo">ファイル情報</param>
        /// <returns></returns>
        Task SaveAsync(FileInfo fileInfo);
    }
}