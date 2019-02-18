using System;
using EngineGL.Core.Utils;

namespace EngineGL.Core
{
    public interface IScene
    {
        void OnLoad();
        void OnUnLoad();

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