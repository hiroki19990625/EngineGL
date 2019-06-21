using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EngineGL.Core;
using EngineGL.Core.Components;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;
using EngineGL.Event.Scene;
using EngineGL.Structs.Drawing;
using EngineGL.Utils;
using Newtonsoft.Json.Linq;

namespace EngineGL.Impl
{
    public class Scene : IScene
    {
        private readonly ConcurrentDictionary<Guid, IObject> _sceneObjects =
            new ConcurrentDictionary<Guid, IObject>();

        private readonly DrawableList _drawables = new DrawableList();

        public string Name { get; set; }
        public event EventHandler<AddObjectEventArgs> AddObjectEvent;
        public event EventHandler<RemoveObjectEventArgs> RemoveObjectEvent;

        public event EventHandler<UpdateEventArgs> Update;
        public event EventHandler<DrawEventArgs> Draw;
        public event EventHandler<GuiRenderEventArgs> GuiRender;

        /// <summary>
        /// 描画レイヤー
        /// </summary>
        public uint Layer { get; set; } = 0;

        //いずれこのプロパティは削除する
        public Colour4 Colour { get; set; }

        public void OnUpdate(double deltaTime)
        {
            Update?.Invoke(this, new UpdateEventArgs(this, deltaTime));

            foreach (IObject obj in _sceneObjects.Values)
            {
                obj.OnUpdate(deltaTime);
            }
        }

        public void OnDraw(double deltaTime)
        {
            Draw?.Invoke(this, new DrawEventArgs(this, deltaTime));
            _drawables.OnDraw(deltaTime);
        }

        public void OnGUI(double deltaTime)
        {
            GuiRender?.Invoke(this, new GuiRenderEventArgs(this, deltaTime));
            foreach (IObject obj in _sceneObjects.Values)
            {
                if (obj is IGuiRenderable)
                {
                    ((IGuiRenderable) obj).OnGUI(deltaTime);
                }
            }
        }

        public Result<IObject[]> GetObjects()
        {
            return Result<IObject[]>.Success(_sceneObjects.Values.ToArray());
        }

        public Result<IObject> GetObject(Guid hash)
        {
            if (_sceneObjects.TryGetValue(hash, out IObject obj))
            {
                return Result<IObject>.Success(obj);
            }

            return Result<IObject>.Fail();
        }

        public Result<T> GetObjectUnsafe<T>(Guid hash) where T : IScene
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
            if (!_sceneObjects.ContainsKey(obj.InstanceGuid))
            {
                AddObjectEventArgs args = new AddObjectEventArgs(this, obj);
                EventManager<AddObjectEventArgs> manager
                    = new EventManager<AddObjectEventArgs>(AddObjectEvent, this, args);
                manager.OnSuccess = ev =>
                {
                    ev.AddObject.Scene = this;
                    return _sceneObjects.TryAdd(ev.AddObject.InstanceGuid, ev.AddObject);
                };

                if (manager.Call())
                {
                    args.AddObject.OnInitialze();
                    if (args.AddObject is IComponentAttachable componentAttachable)
                    {
                        foreach (IComponent component in componentAttachable.GetComponents().Value)
                        {
                            if (component is IDrawableComponent drawableComponent)
                            {
                                _drawables.Add(drawableComponent.InstanceGuid, drawableComponent);
                            }
                            else if (component is IDrawable drawable)
                            {
                                // TODO 削除予定
                                _drawables.Add(args.AddObject.InstanceGuid, drawable);
                            }
                        }
                    }

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
                return Result<IObject>.Success(AddObject((IObject) ins).Value);
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

        public Result<IObject> RemoveObject(Guid hash)
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

                    if (args.RemoveObject is IComponentAttachable componentAttachable)
                    {
                        foreach (IComponent component in componentAttachable.GetComponents().Value)
                        {
                            if (component is IDrawableComponent)
                            {
                                _drawables.Remove(component.InstanceGuid);
                            }
                        }
                    }

                    return Result<IObject>.Success(args.RemoveObject);
                }
                else
                    return Result<IObject>.Fail();
            }

            return Result<IObject>.Fail();
        }

        public Result<IObject> RemoveObject(IObject obj)
        {
            return RemoveObject(obj.InstanceGuid);
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

        public Result<IObject[]> RemoveObjects(params Guid[] hashs)
        {
            List<IObject> list = new List<IObject>();
            foreach (Guid hash in hashs)
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

        public void Save(string filePath)
        {
            string json = this.OnSerializeJson().ToString();

            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            File.WriteAllText(filePath, json);
        }

        public void Save(FileInfo file)
        {
            Save(file.FullName);
        }

        public async Task SaveAsync(string filePath)
        {
            string json = this.OnSerializeJson().ToString();

            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            File.WriteAllText(filePath, json);
        }

        public async Task SaveAsync(FileInfo fileInfo)
        {
            await SaveAsync(fileInfo.FullName);
        }

        public void OnSerialize()
        {
            throw new NotImplementedException();
        }

        public void OnDeserialize<T>(T data)
        {
            throw new NotImplementedException();
        }

        public JObject OnSerializeJson()
        {
            JObject cls = new JObject();
            JArray array = new JArray();
            foreach (IObject obj in _sceneObjects.Values)
            {
                array.Add(obj.OnSerializeJson());
            }

            cls["sceneObjects"] = array;
            cls["name"] = new JValue(Name);

            return cls;
        }

        public void OnDeserializeJson(JObject obj)
        {
            JArray array = (JArray) obj["sceneObjects"];
            foreach (JToken token in array)
            {
                if (token is JObject jObj)
                {
                    IObject ins = (IObject) Activator.CreateInstance(Type.GetType(
                        Assembly.CreateQualifiedName(jObj["assembly"].Value<string>(), jObj["type"].Value<string>())));
                    ins.OnDeserializeJson(jObj);
                    AddObject(ins);
                }
            }

            Name = obj["name"].Value<string>();
        }
    }
}