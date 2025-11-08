using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmallBallBigPlane
{
    public interface ISceneLoader
    {
        AsyncOperation LoadSceneAsync(SceneLoader.Scene scene);
    }

    public class SceneLoader : ISceneLoader
    {
        public enum Scene
        {
            MainMenuScene,
            LoadingScene,
            GameScene,
        }

        public AsyncOperation LoadSceneAsync(Scene scene)
        {
            return SceneManager.LoadSceneAsync((int)scene);
        }
    }
}