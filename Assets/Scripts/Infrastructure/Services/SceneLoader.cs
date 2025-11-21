using System;
using System.Collections;
using SmallBallBigPlane.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmallBallBigPlane.Infrastructure.Services
{
    public class SceneLoader : IService
    {
        public enum Scene
        {
            BootstrapScene,
            GameScene,
        }
        
        public void Load(Scene scene, Action onLoaded = null)
        {
            CoroutinesHandler.StartRoutine(LoadSceneAsync(scene.ToString(), onLoaded));
        }

        private IEnumerator LoadSceneAsync(string sceneName, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation loadSceneAsync =
                SceneManager.LoadSceneAsync(sceneName, new LoadSceneParameters(LoadSceneMode.Single));

            yield return loadSceneAsync;

            onLoaded?.Invoke();
        }
    }
}