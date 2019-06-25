using System;
using System.Diagnostics;
using EngineGL.Impl;
using EngineGL.Impl.DrawableComponents.Shape3D;
using NUnit.Framework;

namespace EngineGL.CITests.Performance
{
    [TestFixture]
    public class ScenePerformance
    {
        [TestCase]
        public void Create()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Scene scene = new Scene();
            Console.WriteLine(stopwatch.Elapsed);
        }

        [TestCase]
        public void AddObject()
        {
            Scene scene = new Scene();
            Stopwatch stopwatch = Stopwatch.StartNew();
            scene.AddObject(new GameObject());
            Console.WriteLine(stopwatch.Elapsed);
        }

        [TestCase]
        public void AddComponentObject()
        {
            SolidBoxObject3D solid = new SolidBoxObject3D();
            Scene scene = new Scene();
            GameObject obj = new GameObject();
            obj.AddComponent(solid);
            Stopwatch stopwatch = Stopwatch.StartNew();
            scene.AddObject(obj);
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}