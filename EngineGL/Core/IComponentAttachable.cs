using System;
using EngineGL.Event.ComponentAttachable;
using EngineGL.Utils;

namespace EngineGL.Core
{
    public interface IComponentAttachable
    {
        event EventHandler<AddComponentEventArgs> AddComponentEvent;
        event EventHandler<RemoveComponentEventArgs> RemoveComponentEvent;

        Result<IComponent[]> GetComponents();
        Result<IComponent> GetComponent(Guid hash);
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