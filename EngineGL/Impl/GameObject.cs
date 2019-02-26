using System;
using System.Collections.Concurrent;
using EngineGL.Core;
using EngineGL.Event.ComponentAttachable;
using EngineGL.Event.LifeCycle;
using EngineGL.Utils;
using OpenTK;

namespace EngineGL.Impl
{
    public class GameObject : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }

        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Bounds { get; set; }

        public ConcurrentDictionary<int, IComponent> AttachedComponents { get; }
            = new ConcurrentDictionary<int, IComponent>();

        public event EventHandler<AddComponentEventArgs> AddComponentEvent;
        public event EventHandler<RemoveComponentEventArgs> RemoveComponentEvent;

        public event EventHandler<InitialzeEventArgs> Initialze;
        public event EventHandler<DestroyEventArgs> Destroy;
        public event EventHandler<UpdateEventArgs> Update;

        public void OnInitialze()
        {
            Initialze?.Invoke(this, new InitialzeEventArgs(this));
        }

        public void OnDestroy()
        {
            Destroy?.Invoke(this, new DestroyEventArgs(this));
        }

        public void OnUpdate()
        {
            Update?.Invoke(this, new UpdateEventArgs(this));
        }

        public Result<IComponent> GetComponent(int hash)
        {
            throw new NotImplementedException();
        }

        public Result<T> GetComponentUnsafe<T>() where T : IComponent
        {
            throw new NotImplementedException();
        }

        public Result<T> GetComponentUnsafe<T>(int hash) where T : IComponent
        {
            throw new NotImplementedException();
        }

        public Result<IComponent> GetComponentUnsafe(Type type)
        {
            throw new NotImplementedException();
        }

        public Result<IComponent> GetComponentUnsafe(Type type, int hash)
        {
            throw new NotImplementedException();
        }

        public Result<IComponent> AddComponent(IComponent component)
        {
            throw new NotImplementedException();
        }

        public Result<T> AddComponentUnsafe<T>(T component) where T : IComponent
        {
            throw new NotImplementedException();
        }

        public Result<T> AddComponentUnsafe<T>() where T : IComponent
        {
            throw new NotImplementedException();
        }

        public Result<IComponent> AddComponentUnsafe(Type type)
        {
            throw new NotImplementedException();
        }

        public Result<IComponent> RemoveComponent(int hash)
        {
            throw new NotImplementedException();
        }

        public Result<IComponent> RemoveComponent(IComponent component)
        {
            throw new NotImplementedException();
        }

        public Result<T> RemoveComponentUnsafe<T>(T component) where T : IComponent
        {
            throw new NotImplementedException();
        }

        public Result<T> RemoveComponentUnsafe<T>() where T : IComponent
        {
            throw new NotImplementedException();
        }

        public Result<T> RemoveComponentUnsafe<T>(int hash) where T : IComponent
        {
            throw new NotImplementedException();
        }

        public Result<IComponent> RemoveComponentUnsafe(Type type)
        {
            throw new NotImplementedException();
        }

        public Result<IComponent> RemoveComponentUnsafe(Type type, int hash)
        {
            throw new NotImplementedException();
        }
    }
}