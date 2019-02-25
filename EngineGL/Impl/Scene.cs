using System;
using System.Collections.Concurrent;
using System.IO;
using EngineGL.Core;
using EngineGL.Core.Utils;
using EngineGL.Event.Scene;

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
            throw new NotImplementedException();
        }

        public Result<T> GetObjectUnsafe<T>(int hash) where T : IScene
        {
            throw new NotImplementedException();
        }

        public Result<IObject> AddObject(IObject obj)
        {
            throw new NotImplementedException();
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