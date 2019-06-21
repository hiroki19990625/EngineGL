using EngineGL.Core;
using EngineGL.Impl;
using EngineGL.Impl.Components;
using EngineGL.Impl.DrawableComponents.Shape2D;
using EngineGL.Impl.Objects;
using EngineGL.Structs.Math;
using EngineGL.Tests.Exec.TestComponents;
using NUnit.Framework;
using OpenTK.Graphics;

namespace EngineGL.Tests.Exec
{
    [TestFixture]
    class NodeTest
    {
        [Test]
        public void PositionTest()
        {
            IGame game = new GameBuilder()
                .SetTitle("Engine Test")
                .SetExceptinDialog(true)
                .SetDebugLogging(true)
                .SetDefaultEvents()
                .Build();

            game.LoadScene(GetPositionScene());

            game.Run(60.0d);
        }

        private Scene GetPositionScene()
        {
            Scene scene = new Scene();

            GameObject gameObject = new GameObject();
            gameObject
                .SetPosition(new Vec3(0f, 0f, 0f))
                .SetBounds(new Vec3(1f, 1f, 0f));
            gameObject.AddComponent(new PlayerComponent
            {
                Bounds = new Vec3(1, 1)
            });

            SolidBoxObject2D box = gameObject.AddComponentUnsafe<SolidBoxObject2D>().Value;
            box.Colour = Color4.White;

            scene.AddObject(gameObject);

            GameObject parent = new GameObject();
            parent
                .SetPosition(new Vec3(2, 0, 0))
                .SetBounds(new Vec3(1, 1, 0));
            parent.AddComponent(new Collision2D
            {
                Bounds = new Vec3(1, 1),
            });

            CircleObject2D circle = parent.AddComponentUnsafe<CircleObject2D>().Value;
            circle.Radius = 0.5f;
            circle.Colour = Color4.Red;
            gameObject.AddChild(parent);

            GameObject cm = new GameObject();
            cm.AddComponent(new StaticCamera());
            cm.Transform.Position = new Vec3(0, 0, -10f);
            scene.AddObject(cm);
            return scene;
        }
    }
}