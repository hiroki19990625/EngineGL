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
        /// 成功した場合、<see cref="Result{T}"/>でラップされた
        /// アタッチされている全てのコンポーネントを返します。
        /// 失敗した場合、<see cref="Result{T}"/> の Fail を返します。
        /// </returns>
        Result<IComponent[]> GetComponents();

        /// <summary>
        /// インスタンスID からアタッチされているコンポーネントを取得します。
        /// </summary>
        /// <param name="hash">インスタンスID</param>
        /// <returns>
        /// 成功した場合、<see cref="Result{T}"/>でラップされた
        /// インスタンスID がら取得したコンポーネントを返します。
        /// 失敗した場合、<see cref="Result{T}"/> の Fail を返します。
        /// </returns>
        Result<IComponent> GetComponent(Guid hash);

        /// <summary>
        /// 型情報からアタッチされているコンポーネントを取得します。
        /// この操作は安全ではありません。
        /// 型情報の型に明示的キャストを行うため例外発生する可能性があります。
        /// また、複数のコンポーネントが存在する場合、最初に見つかったコンポーネントを返すため、
        /// 個々のコンポーネントを取得する場合、<seealso cref="GetComponentUnsafe{T}(Guid)"/>
        /// メソッドを利用してください。
        /// </summary>
        /// <typeparam name="T"><see cref="IComponent"/>型の型引数</typeparam>
        /// <returns></returns>
        Result<T> GetComponentUnsafe<T>() where T : IComponent;

        Result<T> GetComponentUnsafe<T>(Guid hash) where T : IComponent;
        Result<IComponent> GetComponentUnsafe(Type type);
        Result<IComponent> GetComponentUnsafe(Type type, Guid hash);

        Result<IComponent> AddComponent(IComponent component);
        Result<T> AddComponentUnsafe<T>(T component) where T : IComponent;
        Result<T> AddComponentUnsafe<T>() where T : IComponent;
        Result<IComponent> AddComponentUnsafe(Type type);

        Result<IComponent> RemoveComponent(Guid hash);
        Result<IComponent> RemoveComponent(IComponent component);
        Result<T> RemoveComponentUnsafe<T>(T component) where T : IComponent;
        Result<T> RemoveComponentUnsafe<T>() where T : IComponent;
        Result<T> RemoveComponentUnsafe<T>(Guid hash) where T : IComponent;
        Result<IComponent> RemoveComponentUnsafe(Type type);
        Result<IComponent> RemoveComponentUnsafe(Type type, Guid hash);
    }
}