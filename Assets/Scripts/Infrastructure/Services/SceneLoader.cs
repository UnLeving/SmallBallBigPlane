using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmallBallBigPlane.Infrastructure.Services
{
    public class SceneLoader : IService
    {
        public enum Scene
        {
            BootstrapScene,
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