using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.Scene;
using EngineGL.Utils;

namespace EngineGL.Core
{
    public interface IScene : INameable, IUpdateable, IDrawable
    {
        event EventHandler<AddObjectEventArgs> AddObjectEvent;
        event EventHandler<RemoveObjectEventArgs> RemoveObjectEvent;

        Result<IObject> GetObject(int hash);
        Result<T> GetObjectUnsafe<T>(int hash) where T : IScene;

        Result<IObject> AddObject(IObject obj);
        Result<T> AddObjectUnsafe<T>(T obj) where T : IObject;
        Result<T> AddObjectUnsafe<T>() where T : IObject;
        Result<IObject> AddObjectUnsafe(Type type);

        Result<IObject[]> AddObjects(params IObject[] objs);
        Result<T[]> AddObjectsUnsafe<T>(params T[] objs) where T : IObject;

        Result<IObject> RemoveObject(int hash);
        Result<IObject> RemoveObject(IObject obj);
        Result<T> RemoveObjectUnsafe<T>(T obj) where T : IObject;

        Result<IObject[]> RemoveObjects(params int[] hashs);
        Result<IObject[]> RemoveObjects(params IObject[] objs);

        void Save(string filePath);
        void Save(FileInfo file);
        Task SaveAsync(string filePath);
        Task SaveAsync(FileInfo fileInfo);
    }
}