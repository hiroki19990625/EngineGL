using System;
using System.IO;
using System.Text;
using EngineGL.Impl;
using EngineGL.Impl.Drawable;
using EngineGL.Impl.Objects;
using EngineGL.Structs.Math;
using EngineGL.Tests.Exec.TestComponents;
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

        [Test, Order(1)]
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

        [Test, Order(2)]
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
            game.LoadScene(hash).Value.Save("scene.json");

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

            scene.AddObject(new PointsObject
            {
                Position = new Vec3(2, 2, 0),
                PointColor = Color4.White,
                PointSize = 10
            });
            scene.AddObject(new CircleObject
            {
                Position = new Vec3(3.5f, 3.5f, 0),
                Bounds = new Vec3(1, 1, 0),
                Radius = 0.5f,
                CircleColor = Color4.Red
            });

            StaticCamera camera = new StaticCamera();
            camera.Position = new Vec3(0, 0, -10f);
            // camera.AddComponent(new ExceptionComponent());
            scene.AddObject(camera);

            scene.Save("scene.json");

            return scene;
        }
    }
}