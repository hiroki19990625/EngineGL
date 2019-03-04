using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using EngineGL.Core;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;
using EngineGL.Event.Scene;
using EngineGL.Utils;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl
{
    [Serializable]
    public class Scene : IScene
    {
        private readonly ConcurrentDictionary<int, IObject> _sceneObjects =
            new ConcurrentDictionary<int, IObject>();

        public string Name { get; set; }
        public event EventHandler<AddObjectEventArgs> AddObjectEvent;
        public event EventHandler<RemoveObjectEventArgs> RemoveObjectEvent;

        public event EventHandler<UpdateEventArgs> Update;
        public event EventHandler<DrawEventArgs> Draw;

        public Scene(string file) : this(new FileInfo(file))
        {
        }

        public Scene(FileInfo file)
        {
        }

        public Scene()
        {
        }

        public void OnUpdate()
        {
            Update?.Invoke(this, new UpdateEventArgs(this));

            foreach (IObject obj in _sceneObjects.Values)
            {
                obj.OnUpdate();
            }
        }

        public void OnDraw()
        {
            Draw?.Invoke(this, new DrawEventArgs(this));

            foreach (IObject obj in _sceneObjects.Values)
            {
                if (obj is IDrawable)
                {
                    GL.PushAttrib(AttribMask.EnableBit);
                    ((IDrawable) obj).OnDraw();
                    GL.PopAttrib();
                }
            }
        }

        public Result<IObject> GetObject(int hash)
        {
            if (_sceneObjects.TryGetValue(hash, out IObject obj))
            {
                return Result<IObject>.Success(obj);
            }

            return Result<IObject>.Fail();
        }

        public Result<T> GetObjectUnsafe<T>(int hash) where T : IScene
        {
            try
            {
                return Result<T>.Success((T) GetObject(hash).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                return Result<T>.Fail(e.ToString());
            }
        }

        public Result<IObject> AddObject(IObject obj)
        {
            if (!_sceneObjects.ContainsKey(obj.GetHashCode()))
            {
                AddObjectEventArgs args = new AddObjectEventArgs(this, obj);
                EventManager<AddObjectEventArgs> manager
                    = new EventManager<AddObjectEventArgs>(AddObjectEvent, this, args);
                manager.OnSuccess = ev => _sceneObjects.TryAdd(ev.AddObject.GetHashCode(), ev.AddObject);

                if (manager.Call())
                {
                    args.AddObject.OnInitialze();
                    return Result<IObject>.Success(args.AddObject);
                }
                else
                    return Result<IObject>.Fail();
            }

            return Result<IObject>.Fail();
        }

        public Result<T> AddObjectUnsafe<T>(T obj) where T : IObject
        {
            try
            {
                return Result<T>.Success((T) AddObject(obj).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                return Result<T>.Fail(e.ToString());
            }
        }

        public Result<T> AddObjectUnsafe<T>() where T : IObject
        {
            return AddObjectUnsafe(Activator.CreateInstance<T>());
        }

        public Result<IObject> AddObjectUnsafe(Type type)
        {
            object ins = Activator.CreateInstance(type);
            if (ins is IObject)
            {
                return Result<IObject>.Success((IObject) ins);
            }

            return Result<IObject>.Fail();
        }

        public Result<IObject[]> AddObjects(params IObject[] objs)
        {
            List<IObject> list = new List<IObject>();
            foreach (IObject obj in objs)
            {
                Result<IObject> o = AddObject(obj);
                if (o.IsSuccess)
                    list.Add(o.Value);
            }

            return list.Count > 0 ? Result<IObject[]>.Success(list.ToArray()) : Result<IObject[]>.Fail();
        }

        public Result<T[]> AddObjectsUnsafe<T>(params T[] objs) where T : IObject
        {
            List<T> list = new List<T>();
            foreach (T obj in objs)
            {
                Result<T> o = AddObjectUnsafe(obj);
                if (o.IsSuccess)
                    list.Add(o.Value);
            }

            return list.Count > 0 ? Result<T[]>.Success(list.ToArray()) : Result<T[]>.Fail();
        }

        public Result<IObject> RemoveObject(int hash)
        {
            if (_sceneObjects.TryGetValue(hash, out IObject obj))
            {
                RemoveObjectEventArgs args = new RemoveObjectEventArgs(this, obj);
                EventManager<RemoveObjectEventArgs> manager
                    = new EventManager<RemoveObjectEventArgs>(RemoveObjectEvent, this, args);
                manager.OnSuccess = ev => _sceneObjects.TryRemove(hash, out obj);

                if (manager.Call())
                {
                    args.RemoveObject.OnDestroy();
                    return Result<IObject>.Success(args.RemoveObject);
                }
                else
                    return Result<IObject>.Fail();
            }

            return Result<IObject>.Fail();
        }

        public Result<IObject> RemoveObject(IObject obj)
        {
            return RemoveObject(obj.GetHashCode());
        }

        public Result<T> RemoveObjectUnsafe<T>(T obj) where T : IObject
        {
            try
            {
                return Result<T>.Success((T) RemoveObject(obj).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                return Result<T>.Fail(e.ToString());
            }
        }

        public Result<IObject[]> RemoveObjects(params int[] hashs)
        {
            List<IObject> list = new List<IObject>();
            foreach (int hash in hashs)
            {
                Result<IObject> obj = RemoveObject(hash);
                if (obj.IsSuccess)
                    list.Add(obj.Value);
            }

            return list.Count > 0 ? Result<IObject[]>.Success(list.ToArray()) : Result<IObject[]>.Fail();
        }

        public Result<IObject[]> RemoveObjects(params IObject[] objs)
        {
            List<IObject> list = new List<IObject>();
            foreach (IObject obj in objs)
            {
                Result<IObject> o = RemoveObject(obj);
                if (o.IsSuccess)
                    list.Add(o.Value);
            }

            return list.Count > 0 ? Result<IObject[]>.Success(list.ToArray()) : Result<IObject[]>.Fail();
        }
    }
}