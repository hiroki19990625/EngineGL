using EngineGL.Core;
using EngineGL.Impl;
using EngineGL.Impl.Components.Physics;
using EngineGL.Impl.DrawableComponents.Shape3D;
using EngineGL.Impl.Objects;
using EngineGL.Structs.Math;
using NUnit.Framework;
using OpenTK.Graphics;

namespace EngineGL.Tests.Exec
{
    [TestFixture]
    public class PhysicsExecTests
    {
        [Test]
        public void Test()
        {
            Scene scene = new Scene();
            GameObject g1 = new GameObject();
            GlobalPhysicsComponent3D physics = new GlobalPhysicsComponent3D();
            g1.AddComponent(physics);
            scene.AddObject(g1);

            GameObject ground = new GameObject();
            ground.SetPosition(new Vec3(-5f, -3f, -10f))
                .SetBounds(new Vec3(10f, 1f, 10f));
            ground.AddComponent(new SolidBoxObject3D
            {
                Colour = Color4.Red
            });
            RigidBody3D rig1 = new RigidBody3D();
            ground.AddComponent(new BoxCollider());
            ground.AddComponent(rig1);
            rig1.RigidBody.IsStatic = true;
            scene.AddObject(ground);
            physics.AddRigidBody(rig1);

            GameObject player = new GameObject();
            player.SetPosition(new Vec3(0, 0f, -5))
                .SetBounds(Vec3.One);
            player.AddComponent(new SolidBoxObject3D
            {
                Colour = Color4.Green
            });
            RigidBody3D rig2 = new RigidBody3D();
            player.AddComponent(new BoxCollider());
            player.AddComponent(rig2);
            scene.AddObject(player);
            physics.AddRigidBody(rig2);

            GameObject camera = new GameObject();
            camera.SetPosition(new Vec3(0, 0, -10));
            camera.AddComponent(new StaticCamera());
            scene.AddObject(camera);

            IGame game = new GameBuilder()
                .SetDefaultEvents()
                .Build();

            game.LoadScene(scene);
            game.Run(60.0f);
        }
    }
}