using System;
using EngineGL.Event.ComponentAttachable;
using EngineGL.Utils;

namespace EngineGL.Core
{
    /// <summary>
    /// コンポーネントがアタッチ出来るオブジェクトを実装します。
    /// </summary>
    public interface IComponentAttachable
    {
        /// <summary>
        /// コンポーネントを追加した時に発火するイベント
        /// </summary>
        event EventHandler<AddComponentEventArgs> AddComponentEvent;

        /// <summary>
        /// コンポーネントが削除された時に発火するイベント
        /// </summary>
        event EventHandler<RemoveComponentEventArgs> RemoveComponentEvent;

        /// <summary>
        /// アタッチされているコンポーネントを全て取得します。
        /// </summary>
        /// <returns>
        /// アタッチされているコンポーネントを全て返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IComponent[]> GetComponents();

        /// <summary>
        /// インスタンスID からアタッチされているコンポーネントを取得します。
        /// </summary>
        /// <param name="hash">インスタンスID</param>
        /// <returns>
        ///　アタッチされているコンポーネント返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IComponent> GetComponent(Guid hash);

        /// <summary>
        /// ジェネリクス型情報からアタッチされているコンポーネントを取得します。
        /// </summary>
        /// <typeparam name="T"><see cref="IComponent"/>型の型引数</typeparam>
        /// <returns>
        ///　取得したコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<T> GetComponentUnsafe<T>() where T : IComponent;

        /// <summary>
        /// ジェネリクス型情報とインスタンスID からアタッチされているコンポーネントを取得します。
        /// </summary>
        /// <param name="hash">インスタンスID</param>
        /// <typeparam name="T"><see cref="IComponent"/>型の型引数</typeparam>
        /// <returns>成功または、失敗する可能性があります。</returns>
        Result<T> GetComponentUnsafe<T>(Guid hash) where T : IComponent;

        /// <summary>
        /// 型情報からアタッチされているコンポーネントを取得します。
        /// </summary>
        /// <param name="type">型情報</param>
        /// <returns>
        ///　取得したコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IComponent> GetComponentUnsafe(Type type);

        /// <summary>
        /// 型情報とインスタンスIDからアタッチされているコンポーネントを取得します。
        /// </summary>
        /// <param name="type">型情報</param>
        /// <param name="hash">インスタンスID</param>
        /// <returns>
        ///　取得したコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IComponent> GetComponentUnsafe(Type type, Guid hash);

        /// <summary>
        /// コンポーネントをアタッチします。
        /// </summary>
        /// <param name="component">アタッチするコンポーネント</param>
        /// <returns>
        /// アタッチしたコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IComponent> AddComponent(IComponent component);

        /// <summary>
        /// コンポーネントをアタッチします。
        /// </summary>
        /// <param name="component">アタッチするコンポーネント</param>
        /// <typeparam name="T"><see cref="IComponent"/>型の型引数</typeparam>
        /// <returns>
        ///　アタッチしたコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<T> AddComponentUnsafe<T>(T component) where T : IComponent;

        /// <summary>
        /// ジェネリクス型情報を基にインスタンスを作成してコンポーネントをアタッチします。
        /// </summary>
        /// <typeparam name="T"><see cref="IComponent"/>型の型引数</typeparam>
        /// <returns>
        ///　アタッチしたコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<T> AddComponentUnsafe<T>() where T : IComponent;

        /// <summary>
        /// 型情報を基にインスタンスを作成してコンポーネントをアタッチします。
        /// </summary>
        /// <param name="type">型情報</param>
        /// <returns>
        ///　アタッチしたコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IComponent> AddComponentUnsafe(Type type);

        /// <summary>
        /// インスタンスID からアタッチされているコンポーネントを削除します。
        /// </summary>
        /// <param name="hash"></param>
        /// <returns>
        ///　削除されたコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IComponent> RemoveComponent(Guid hash);

        /// <summary>
        /// インスタンスからアタッチされているコンポーネントを削除します。
        /// </summary>
        /// <param name="component">コンポーネントのインスタンス</param>
        /// <returns>
        ///　削除されたコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IComponent> RemoveComponent(IComponent component);

        /// <summary>
        /// インスタンスからアタッチされているコンポーネントを削除します。
        /// </summary>
        /// <param name="component">コンポーネントのインスタンス</param>
        /// <typeparam name="T"><see cref="IComponent"/>型の型引数</typeparam>
        /// <returns>
        ///　削除されたコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<T> RemoveComponentUnsafe<T>(T component) where T : IComponent;

        /// <summary>
        /// ジェネリクス型情報からアタッチされているコンポーネントを削除します。
        /// </summary>
        /// <typeparam name="T"><see cref="IComponent"/>型の型引数</typeparam>
        /// <returns>
        ///　削除されたコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<T> RemoveComponentUnsafe<T>() where T : IComponent;

        /// <summary>
        /// ジェネリクス型情報とインスタンスID からアタッチされているコンポーネントを削除します。
        /// </summary>
        /// <param name="hash">インスタンスID</param>
        /// <typeparam name="T"><see cref="IComponent"/>型の型引数</typeparam>
        /// <returns>
        ///　削除されたコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<T> RemoveComponentUnsafe<T>(Guid hash) where T : IComponent;

        /// <summary>
        /// 型情報からアタッチされているコンポーネントを削除します。
        /// </summary>
        /// <param name="type">型情報</param>
        /// <returns>
        ///　削除されたコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IComponent> RemoveComponentUnsafe(Type type);

        /// <summary>
        /// 型情報とインスタンスID からアタッチされているコンポーネントを削除します。
        /// </summary>
        /// <param name="type">型情報</param>
        /// <param name="hash">インスタンスID</param>
        /// <returns>
        ///　削除されたコンポーネントを返します。
        /// <para>成功または、失敗する可能性があります。</para>
        /// </returns>
        Result<IComponent> RemoveComponentUnsafe(Type type, Guid hash);
    }
}