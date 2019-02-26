using System;
using System.Collections.Concurrent;
using EngineGL.Event.Scene;
using EngineGL.Utils;

namespace EngineGL.Core
{
    public interface IScene : INameable
    {
        ConcurrentDictionary<int, IObject> SceneObjects { get; }

        event EventHandler<AddObjectEventArgs> AddObjectEvent;
        event EventHandler<RemoveObjectEventArgs> RemoveObjectEvent;

        Result<IObject> GetObject(int hash);
        Result<T> GetObjectUnsafe<T>(int hash) where T : IScene;

        Result<IObject> AddObject(IObject obj);
        Result<T> AddObjectUnsafe<T>(T obj) where T : IObject;
        Result<T> AddObjectUnsafe<T>() where T : IObject;
        Result<IObject> AddObjectUnsafe(Type type);

        Result<IObject[]> AddObjects(params IObject[] obj);
        Result<T[]> AddObjectsUnsafe<T>(params T[] objs);

        Result<IObject> RemoveObject(int hash);
        Result<IObject> RemoveObject(IObject obj);
        Result<T> RemoveObjectUnsafe<T>(T obj) where T : IObject;

        Result<IObject[]> RemoveObjects(params int[] hashs);
        Result<IObject[]> RemoveObjects(params IObject[] objs);
    }
}