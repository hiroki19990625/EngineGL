using System;
using EngineGL.Impl;
using EngineGL.Impl.Drawable;
using NUnit.Framework;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Tests.Impl
{
    [TestFixture]
    public class GameTests
    {
        //[Test]
        public void OpenWindow()
        {
            Game game = new Game();
            game.Run(60.0d);
        }

        [Test]
        public void DrawLine()
        {
            Game game = new Game();
            //game.WindowState = WindowState.Fullscreen;
            game.Title = "Engine Test";
            game.Load += Game_OnLoad;
            game.Resize += (sender, args) =>
            {
                GL.Viewport(game.ClientRectangle);
                GL.MatrixMode(MatrixMode.Projection);
                Matrix4 projection =
                    Matrix4.CreatePerspectiveFieldOfView((float) Math.PI / 4, (float) game.Width / (float) game.Height,
                        1.0f,
                        64.0f);
                GL.LoadMatrix(ref projection);
            };
            game.RenderFrame += Game_OnRenderFrame;

            Scene scene = new Scene();
            scene.AddObject(new LineObject
            {
                LineColor = Color4.Red,
                Position = new Vector3(-0.1f, -0.1f, 2f),
                Bounds = new Vector3(0.1f, 0.1f, 2f),
                LineWidth = 5f
            });
            scene.AddObject(new LineObject
            {
                LineColor = Color4.Red,
                Position = new Vector3(-0.1f, 0.1f, 2f),
                Bounds = new Vector3(0.1f, -0.1f, 2f),
                LineWidth = 10f
            });
            game.LoadScene(scene);

            game.Run(60.0d);
        }

        private void Game_OnRenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitX);
            GL.LoadMatrix(ref modelview);
        }

        private void Game_OnLoad(object sender, EventArgs e)
        {
            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);
        }
    }
}