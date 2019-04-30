using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using EngineGL.Core;
using EngineGL.Event.ComponentAttachable;
using EngineGL.Structs.Math;
using EngineGL.Utils;
using Newtonsoft.Json;

namespace EngineGL.Impl
{
    public class GameObject : Object, IGameObject
    {
        [JsonProperty("Components")] private readonly ConcurrentDictionary<Guid, IComponent> _attachedComponents =
            new ConcurrentDictionary<Guid, IComponent>();

        public Vec3 Position { get; set; }
        public Vec3 Rotation { get; set; }
        public Vec3 Bounds { get; set; }
        public Vec3 Scale { get; set; } = Vec3.One;

        public event EventHandler<AddComponentEventArgs> AddComponentEvent;
        public event EventHandler<RemoveComponentEventArgs> RemoveComponentEvent;

        public override void OnUpdate(double deltaTime)
        {
            base.OnUpdate(deltaTime);

            foreach (IComponent component in _attachedComponents.Values)
            {
                component.OnUpdate(deltaTime);
            }
        }

        public Result<IComponent[]> GetComponents()
        {
            return Result<IComponent[]>.Success(_attachedComponents.Values.ToArray());
        }

        public Result<IComponent> GetComponent(Guid hash)
        {
            if (_attachedComponents.TryGetValue(hash, out IComponent component))
                return Result<IComponent>.Success(component);

            return Result<IComponent>.Fail();
        }

        public Result<T> GetComponentUnsafe<T>() where T : IComponent
        {
            foreach (KeyValuePair<Guid, IComponent> pair in _attachedComponents)
            {
                IComponent component = pair.Value;
                if (component is T)
                    return Result<T>.Success((T) component);
            }

            return Result<T>.Fail();
        }

        public Result<T> GetComponentUnsafe<T>(Guid hash) where T : IComponent
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
            foreach (KeyValuePair<Guid, IComponent> pair in _attachedComponents)
            {
                IComponent component = pair.Value;
                if (component.GetType().FullName == type.FullName)
                    return Result<IComponent>.Success(component);
            }

            return Result<IComponent>.Fail();
        }

        public Result<IComponent> GetComponentUnsafe(Type type, Guid hash)
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
            Guid hash = component.InstanceGuid;
            if (!_attachedComponents.ContainsKey(hash))
            {
                AddComponentEventArgs args =
                    new AddComponentEventArgs(this, component);
                EventManager<AddComponentEventArgs> manager =
                    new EventManager<AddComponentEventArgs>(AddComponentEvent, this, args);
                manager.OnSuccess = ev => _attachedComponents.TryAdd(ev.AddComponent.InstanceGuid, ev.AddComponent);

                if (manager.Call())
                {
                    component.ParentObject = this;
                    component.OnInitialze();
                    return Result<IComponent>.Success(args.AddComponent);
                }
                else
                    return Result<IComponent>.Fail();
            }

            return Result<IComponent>.Fail();
        }

        public Result<T> AddComponentUnsafe<T>(T component) where T : IComponent
        {
            try
            {
                return Result<T>.Success((T) AddComponent(component).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                return Result<T>.Fail(e.ToString());
            }
        }

        public Result<T> AddComponentUnsafe<T>() where T : IComponent
        {
            T instance = Activator.CreateInstance<T>();
            return AddComponentUnsafe(instance);
        }

        public Result<IComponent> AddComponentUnsafe(Type type)
        {
            try
            {
                IComponent instance = (IComponent) Activator.CreateInstance(type);
                return AddComponent(instance);
            }
            catch (InvalidCastException e)
            {
                return Result<IComponent>.Fail(e.Message);
            }
        }

        public Result<IComponent> RemoveComponent(Guid hash)
        {
            if (_attachedComponents.TryGetValue(hash, out IComponent component))
            {
                RemoveComponentEventArgs args =
                    new RemoveComponentEventArgs(this, component);
                EventManager<RemoveComponentEventArgs> manager =
                    new EventManager<RemoveComponentEventArgs>(RemoveComponentEvent, this, args);
                manager.OnSuccess = ev =>
                    _attachedComponents.TryRemove(ev.RemoveComponent.InstanceGuid, out component);

                if (manager.Call())
                {
                    args.RemoveComponent.OnDestroy();
                    return Result<IComponent>.Success(args.RemoveComponent);
                }
                else
                    return Result<IComponent>.Fail();
            }

            return Result<IComponent>.Fail();
        }

        public Result<IComponent> RemoveComponent(IComponent component)
        {
            return RemoveComponent(component.InstanceGuid);
        }

        public Result<T> RemoveComponentUnsafe<T>(T component) where T : IComponent
        {
            try
            {
                return Result<T>.Success((T) RemoveComponent(component).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                return Result<T>.Fail(e.ToString());
            }
        }

        public Result<T> RemoveComponentUnsafe<T>() where T : IComponent
        {
            foreach (IComponent component in _attachedComponents.Values)
                if (component is T)
                    return RemoveComponentUnsafe((T) component);

            return Result<T>.Fail();
        }

        public Result<T> RemoveComponentUnsafe<T>(Guid hash) where T : IComponent
        {
            if (_attachedComponents.TryGetValue(hash, out IComponent component))
                if (component is T)
                    return RemoveComponentUnsafe((T) component);

            return Result<T>.Fail();
        }

        public Result<IComponent> RemoveComponentUnsafe(Type type)
        {
            foreach (IComponent component in _attachedComponents.Values)
                if (component.GetType().FullName == type.FullName)
                    return Result<IComponent>.Success(component);

            return Result<IComponent>.Fail();
        }

        public Result<IComponent> RemoveComponentUnsafe(Type type, Guid hash)
        {
            if (_attachedComponents.TryGetValue(hash, out IComponent component))
                if (component.GetType().FullName == type.FullName)
                    return Result<IComponent>.Success(component);

            return Result<IComponent>.Fail();
        }
    }
}