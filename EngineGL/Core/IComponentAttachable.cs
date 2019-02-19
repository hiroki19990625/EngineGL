using System;
using System.Collections.Concurrent;
using EngineGL.Core.Utils;

namespace EngineGL.Core
{
    public interface IComponentAttachable
    {
        ConcurrentDictionary<int, IComponent> AttachedComponents { get; }

        Result<IComponent> GetComponent(int hash);
        Result<T> GetComponentUnsafe<T>() where T : IComponent;
        Result<T> GetComponentUnsafe<T>(int hash) where T : IComponent;
        Result<IComponent> GetComponentUnsafe(Type type);
        Result<IComponent> GetComponentUnsafe(Type type, int hash);

        Result<IComponent> AddComponent(IComponent component);
        Result<T> AddComponentUnsafe<T>(T component) where T : IComponent;
        Result<T> AddComponentUnsafe<T>() where T : IComponent;
        Result<IComponent> AddComponentUnsafe(Type type);

        Result<IComponent> RemoveComponent(int hash);
        Result<IComponent> RemoveComponent(IComponent component);
        Result<T> RemoveComponentUnsafe<T>(T component) where T : IComponent;
        Result<T> RemoveComponentUnsafe<T>() where T : IComponent;
        Result<T> RemoveComponentUnsafe<T>(int hash) where T : IComponent;
        Result<IComponent> RemoveComponentUnsafe(Type type);
        Result<IComponent> RemoveComponentUnsafe(Type type, int hash);
    }
}