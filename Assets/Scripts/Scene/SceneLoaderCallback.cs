using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class SceneLoaderCallback : MonoBehaviour
    {
        private bool isFirstUpdate = true;

        [Inject] private ISceneLoader _sceneLoader;
    
        private void Update()
        {
            if (!isFirstUpdate) return;
        
            isFirstUpdate = false;

            _sceneLoader.LoaderCallback();
        }
    }
}