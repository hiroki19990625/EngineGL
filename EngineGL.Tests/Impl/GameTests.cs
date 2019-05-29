using System;
using System.Threading;
using System.Threading.Tasks;
using EngineGL.Impl;
using EngineGL.Impl.Drawable;
using EngineGL.Utils;
using NUnit.Framework;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Tests.Impl
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void SceneTest()
        {
            Game game = new Game();
            Scene loadCancel = new Scene();
            Scene unloadCancel = new Scene();

            game.SceneEvents.LoadSceneEvent += (sender, args) =>
            {
                if (args.LoadScene.GetHashCode() == loadCancel.GetHashCode())
                {
                    args.IsCanceled = true;
                }
            };
            game.SceneEvents.UnloadSceneEvent += (sender, args) =>
            {
                if (args.UnloadScene.GetHashCode() == unloadCancel.GetHashCode())
                {
                    args.IsCanceled = true;
                }
            };
            // キャンセル対象なので失敗
            Assert.False(game.LoadScene(loadCancel).IsSuccess);
            // キャンセルの対象なので取得に失敗
            Assert.False(game.GetScene(loadCancel.GetHashCode()).IsSuccess);
            // キャンセル対象のシーンではないのでロードには成功
            Assert.True(game.LoadScene(unloadCancel).IsSuccess);
            // ロードに成功しているので取得に成功
            Assert.True(game.GetScene(unloadCancel.GetHashCode()).IsSuccess);
            // キャンセルの対象なのでアンロードに失敗
            Assert.False(game.UnloadScene(unloadCancel).IsSuccess);
            // アンロードがキャンセルされているので取得に成功
            Assert.True(game.GetScene(unloadCancel.GetHashCode()).IsSuccess);

            Scene scene = new Scene();
            Scene scene2 = new Scene();
            Assert.True(game.LoadScene(scene).IsSuccess);
            // 既に追加されているので失敗
            Assert.False(game.LoadScene(scene).IsSuccess);
            // ロードに成功しているので取得に成功
            Assert.True(game.GetScene(scene.GetHashCode()).IsSuccess);
            Assert.True(game.UnloadScene(scene).IsSuccess);
            // 既にアンロードされているので失敗
            Assert.False(game.UnloadScene(scene).IsSuccess);
            // アンロードされているので取得に失敗
            Assert.False(game.GetScene(scene.GetHashCode()).IsSuccess);
            Assert.True(game.LoadScene(scene).IsSuccess);
            Assert.True(game.GetScene(scene.GetHashCode()).IsSuccess);
            Assert.True(game.LoadScene(scene2).IsSuccess);
            Assert.True(game.GetScene(scene2.GetHashCode()).IsSuccess);
            Assert.True(game.UnloadScenes());
            // 全てアンロードしたので失敗
            Assert.False(game.GetScene(scene.GetHashCode()).IsSuccess);
            Assert.False(game.GetScene(scene2.GetHashCode()).IsSuccess);

            game.LoadScene(scene);
            game.LoadNextScene(scene2);
            // 前にロードしたシーンの取得に失敗
            Assert.False(game.GetScene(scene.GetHashCode()).IsSuccess);
            // 新しくロードしたシーンの取得に成功
            Assert.True(game.GetScene(scene2.GetHashCode()).IsSuccess);
        }

        [Test]
        public void SceneUnsafeTest()
        {
            Game game = new Game();
            Scene scene = new Scene();

            // ロードに成功
            Assert.True(game.LoadSceneUnsafe(scene).IsSuccess);
            // 既にロードしたシーンなので失敗
            Assert.False(game.LoadSceneUnsafe(scene).IsSuccess);
            // 取得に成功
            Assert.True(game.GetSceneUnsafe<Scene>(scene.GetHashCode()).IsSuccess);
            // アンロードに成功
            Assert.True(game.UnloadSceneUnsafe(scene).IsSuccess);
            // 既にアンロードされているので失敗
            Assert.False(game.UnloadSceneUnsafe(scene).IsSuccess);
        }
    }
}