using EngineGL.Core.LifeCycle;

namespace EngineGL.Core.Components
{
    /// <summary>
    /// 描画プロセスが実行されるコンポーネントを実装します。
    /// </summary>
    public interface IDrawableComponent : IComponent, IDrawable
    {
    }
}