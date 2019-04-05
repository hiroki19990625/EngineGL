using System;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using EngineGL.Core;
using EngineGL.Impl;
using EngineGL.Impl.Components;
using EngineGL.Impl.Drawable;
using EngineGL.Impl.Objects;
using EngineGL.Structs;
using EngineGL.Structs.Math;
using EngineGL.Tests.Exec.TestComponents;
using EngineGL.Tests.Exec.TestObjects;
using EngineGL.Utils;
using NLog.Config;
using NLog.Targets;
using NUnit.Framework;
using OpenTK.Graphics;

namespace EngineGL.Tests.Exec
{
    [TestFixture]
    public class GamrExecTest
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var dir = Path.GetDirectoryName(typeof(GamrExecTest).Assembly.Location);
            Environment.CurrentDirectory = dir;
        }

        [Test]
        public void ExecGame()
        {
            Game game = new Game();
            //game.WindowState = WindowState.Fullscreen;
            game.Title = "Engine Test";
            game.ExceptionDialog = true;
            game.DebugLogging = true;
            game.LoggingConfiguration = GetLoggingConfiguration();
            game.Load += (sender, args) => game.LoadDefaultFunc();
            game.Resize += (sender, args) => game.AdjustResize();
            game.RenderFrame += (sender, args) => game.DrawDefaultFunc(args);

            game.LoadScene(GetInitScene());
            game.LoadScene(new Scene());

            game.Run(60.0d);
        }

        [Test]
        public void ExecGame2()
        {
            Game game = new Game();
            //game.WindowState = WindowState.Fullscreen;
            game.Title = "Engine Test";
            game.ExceptionDialog = true;
            game.DebugLogging = true;
            game.LoggingConfiguration = GetLoggingConfiguration();
            game.Load += (sender, args) => game.LoadDefaultFunc();
            game.Resize += (sender, args) => game.AdjustResize();
            game.RenderFrame += (sender, args) => game.DrawDefaultFunc(args);

            int hash = game.PreLoadScene<Scene>("scene.json").Value;
            game.LoadScene(hash);

            game.Run(60.0d);
        }

        private LoggingConfiguration GetLoggingConfiguration()
        {
            LoggingConfiguration configuration = new LoggingConfiguration();
            configuration.AddTarget(new FileTarget("LogFile")
            {
                Layout = "${longdate} ${logger} ${message}${exception:format=ToString}",
                FileName = "${basedir}/logs/Debug.log",
                Encoding = Encoding.UTF8
            });
            configuration.AddRuleForAllLevels("LogFile");

            return configuration;
        }

        private Scene GetInitScene()
        {
            Scene scene = new Scene();
            scene.AddObject(new SolidBoxObject2D
            {
                BoxColor = Color4.White,
                Position = new Vec3(3f, 3f, 0f),
                Bounds = new Vec3(1f, 1f, 0f)
            });

            CollisionInspector inspector = new CollisionInspector();
            inspector.Tag = "GUI";
            scene.AddObject(inspector);

            SolidPolygonObject2D poly = new SolidPolygonObject2D
            {
                PoligonColor = Color4.Gold,
                Position = new Vec3(-3f, -3f, 0f)
            };
            poly.Points.Add(new Vec3(1f, 1.5f, 0));
            poly.Points.Add(new Vec3(0f, 0, 0));
            poly.Points.Add(new Vec3(2f, 0, 0));
            poly.AddComponentUnsafe<PlayerComponent>();
            scene.AddObject(poly);

            /*scene.AddObject(new LineObject
            {
                LineColor = Color4.Red,
                Position = new Vec3(-1f, -1f, 0f),
                Bounds = new Vec3(2f, 2f, 0f)
            });

            scene.AddObject(new StippleLineObject
            {
                LineColor = Color4.Aqua,
                Position = new Vec3(-1f, 1f, 0f),
                Bounds = new Vec3(2f, -2f, 0f),
                Factor = 1,
                Pattern = 0x7777
            });

            scene.AddObject(new StippleLineObject
            {
                LineColor = Color4.Green,
                Position = new Vec3(0f, 1f, 0f),
                Bounds = new Vec3(0f, -2f, 0),
                Factor = 1,
                Pattern = 0xf0af
            });*/

            StaticCamera camera = new StaticCamera();
            camera.Position = new Vec3(0, 0, -10f);
            // camera.AddComponent(new ExceptionComponent());
            scene.AddObject(camera);

            scene.Save("scene.json");

            return scene;
        }
    }
}