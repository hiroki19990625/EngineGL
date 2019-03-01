using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

        public virtual void OnInitialze()
        {
            Initialze?.Invoke(this, new InitialzeEventArgs(this));
        }

        public virtual void OnDestroy()
        {
            Destroy?.Invoke(this, new DestroyEventArgs(this));
        }

        public virtual void OnUpdate()
        {
            Update?.Invoke(this, new UpdateEventArgs(this));
        }

        public Result<IComponent> GetComponent(int hash)
        {
            if (AttachedComponents.TryGetValue(hash, out IComponent component))
                return Result<IComponent>.Success(component);

            return Result<IComponent>.Fail();
        }

        public Result<T> GetComponentUnsafe<T>() where T : IComponent
        {
            foreach (KeyValuePair<int, IComponent> pair in AttachedComponents)
            {
                IComponent component = pair.Value;
                if (component is T)
                    return Result<T>.Success((T) component);
            }

            return Result<T>.Fail();
        }

        public Result<T> GetComponentUnsafe<T>(int hash) where T : IComponent
        {
            try
            {
                return Result<T>.Success((T) GetComponent(hash).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                return Result<T>.Fail(e.ToString());
            }
        }

        public Result<IComponent> GetComponentUnsafe(Type type)
        {
            foreach (KeyValuePair<int, IComponent> pair in AttachedComponents)
            {
                IComponent component = pair.Value;
                if (component.GetType().FullName == type.FullName)
                    return Result<IComponent>.Success(component);
            }

            return Result<IComponent>.Fail();
        }

        public Result<IComponent> GetComponentUnsafe(Type type, int hash)
        {
            try
            {
                IComponent component = GetComponent(hash).Value;
                if (component.GetType().FullName == type.FullName)
                    return Result<IComponent>.Success(component);
            }
            catch (Exception e) when (e is InvalidOperationException)
            {
                return Result<IComponent>.Fail(e.ToString());
            }

            return Result<IComponent>.Fail();
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