using System;
using System.Collections.Concurrent;
using System.IO;
using EngineGL.Core;
using EngineGL.Event.Scene;
using EngineGL.Utils;

namespace EngineGL.Impl
{
    public class Scene : IScene
    {
        public string Name { get; set; }
        public ConcurrentDictionary<int, IObject> SceneObjects { get; }
        public event EventHandler<AddObjectEventArgs> AddObjectEvent;
        public event EventHandler<RemoveObjectEventArgs> RemoveObjectEvent;

        public Scene(string file) : this(new FileInfo(file))
        {
        }

        public Scene(FileInfo file)
        {
        }

        public Result<IObject> GetObject(int hash)
        {
            if (SceneObjects.TryGetValue(hash, out IObject obj))
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
            if (!SceneObjects.ContainsKey(obj.GetHashCode()))
            {
                AddObjectEventArgs args = new AddObjectEventArgs(this, obj);
                EventManager<AddObjectEventArgs> manager
                    = new EventManager<AddObjectEventArgs>(AddObjectEvent, this, args);
                manager.OnSuccess = ev => SceneObjects.TryAdd(ev.AddObject.GetHashCode(), ev.AddObject);

                if (manager.Call())
                    return Result<IObject>.Success(args.AddObject);
                else
                    return Result<IObject>.Fail();
            }

            return Result<IObject>.Fail();
        }

        public Result<T> AddObjectUnsafe<T>(T obj) where T : IObject
        {
            throw new NotImplementedException();
        }

        public Result<T> AddObjectUnsafe<T>() where T : IObject
        {
            throw new NotImplementedException();
        }

        public Result<IObject> AddObjectUnsafe(Type type)
        {
            throw new NotImplementedException();
        }

        public Result<IObject[]> AddObjects(params IObject[] obj)
        {
            throw new NotImplementedException();
        }

        public Result<T[]> AddObjectsUnsafe<T>(params T[] objs)
        {
            throw new NotImplementedException();
        }

        public Result<IObject> RemoveObject(int hash)
        {
            throw new NotImplementedException();
        }

        public Result<IObject> RemoveObject(IObject obj)
        {
            throw new NotImplementedException();
        }

        public Result<T> RemoveObjectUnsafe<T>(T obj) where T : IObject
        {
            throw new NotImplementedException();
        }

        public Result<IObject[]> RemoveObjects(params int[] hashs)
        {
            throw new NotImplementedException();
        }

        public Result<IObject[]> RemoveObjects(params IObject[] objs)
        {
            throw new NotImplementedException();
        }
    }
}