using System;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using EngineGL.Core.Resource;
using EngineGL.Impl;
using EngineGL.Impl.Components;
using EngineGL.Impl.Drawable;
using EngineGL.Impl.Drawable.Shape2D;
using EngineGL.Impl.Drawable.Shape3D;
using EngineGL.Impl.Objects;
using EngineGL.Impl.Resource;
using EngineGL.Structs.Math;
using EngineGL.Tests.Exec.TestComponents;
using EngineGL.Utils;
using NLog.Config;
using NLog.Targets;
using NUnit.Framework;
using OpenTK.Audio.OpenAL;
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
            Game game = new GameBuilder()
                .SetTitle("Engine Test")
                .SetExceptinDialog(true)
                .SetDebugLogging(true)
                .SetLoggingConfiguration(GetLoggingConfiguration())
                .SetDefaultEvents()
                .Build();

            game.LoadScene(GetInitScene());
            game.LoadScene(new Scene());

            game.Run(60.0d);
        }

        [Test, Order(2)]
        public void ExecGame2()
        {
            Game game = new GameBuilder()
                .SetTitle("Engine Test")
                .SetExceptinDialog(true)
                .SetDebugLogging(true)
                .SetLoggingConfiguration(GetLoggingConfiguration())
                .SetDefaultEvents()
                .Build();

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
            Dialog.Show("Test");

            Scene scene = new Scene();
            SolidBoxObject2D box = new SolidBoxObject2D();
            box.BoxColor = Color4.White;
            box
                .SetPosition(new Vec3(3f, 3f, 0f))
                .SetBounds(new Vec3(1f, 1f, 0f));
            box.AddComponent(new PlayerComponent()
            {
                Bounds = new Vec3(1, 1)
            });
            box.AddComponent(new RotateComponent());
            scene.AddObject(box);


            SolidBoxObject3D boxObject3D = new SolidBoxObject3D
            {
                BoxColor = Color4.Pink
            };
            boxObject3D.Layer = 99;
            boxObject3D.SetPosition(new Vec3(-2f, -1f, 0f));
            boxObject3D.SetBounds(new Vec3(2f, 2f, 5f));
            boxObject3D.AddComponent(new RotateComponent());
            scene.AddObject(boxObject3D);

            SolidPolygonObject2D poly = new SolidPolygonObject2D
            {
                PoligonColor = Color4.Gold,
            };
            poly.SetPosition(new Vec3(-3f, -3f, 0f));
            poly.Layer = 1;
            poly.Points.Add(new Vec3(1f, 1.5f, 0));
            poly.Points.Add(new Vec3(0f, 0, 0));
            poly.Points.Add(new Vec3(2f, 0, 0));
            poly.AddComponent(new RotateComponent());
            scene.AddObject(poly);

            SolidPolygonObject3D poly3 = new SolidPolygonObject3D
            {
                PoligonColor = Color4.Gray,
            };

            poly3.SetPosition(new Vec3(1f, 1f, 0f));
            poly3.SetBounds(new Vec3(0, 0, 3f));
            poly3.Layer = 1;
            poly3.Points.Add(new Vec3(0, 0));
            poly3.Points.Add(new Vec3(1f, 0));
            poly3.Points.Add(new Vec3(1.5f, 0.86f));
            poly3.Points.Add(new Vec3(1.0f, 1.72f));
            poly3.Points.Add(new Vec3(0, 1.72f));
            poly3.Points.Add(new Vec3(-0.5f, 0.87f));
            poly3.AddComponent(new RotateComponent());
            scene.AddObject(poly3);

            scene.AddObject(new PointsObject2D
                {
                    PointColor = Color4.White,
                    PointSize = 10
                }
                .SetPosition(new Vec3(2, 2, 0))
            );
            CircleObject2D circle = new CircleObject2D()
            {
                Radius = 0.5f,
                CircleColor = Color4.Red
            };
            circle.AddComponent(new Collision2D
            {
                Bounds = new Vec3(1, 1),
                Offset = new Vec3(-0.5f, -0.5f)
            });
            circle
                .SetPosition(new Vec3(3.5f, 1f, 0))
                .SetBounds(new Vec3(1, 1, 0));
            scene.AddObject(circle);

            scene.AddObject(new RawTexture2D("Images/download.png")
                .SetBounds(new Vec3(3, 3, 0))
                .SetPosition(new Vec3(-2, -2, 0)));
            scene.AddObject(new TextRenderer(100, 100)
                {
                    FontColor = Color.Red,
                    Text = "Hello",
                    Tag = "Text"
                }
                .SetBounds(new Vec3(1, 1, 0))
                .SetPosition(new Vec3(0.5f, 0, 0))
            );

            IAudio audio = ResourceManager.LoadWave("Sounds/Mixdown2.wav");
            audio.SetLoop(true);
            audio.Play();

            StaticCamera camera = new StaticCamera();
            camera.Transform.Position = new Vec3(0, 0, -10f);
            // camera.AddComponent(new ExceptionComponent());
            scene.AddObject(camera);
            scene.Save("scene.json");

            return scene;
        }
    }
}