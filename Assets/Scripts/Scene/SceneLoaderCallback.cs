using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class SceneLoaderCallback : MonoBehaviour
    {
        private bool isFirstUpdate = true;

        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            this._sceneLoader = sceneLoader;
        }

        private void Update()
        {
            if (!isFirstUpdate) return;
        
            isFirstUpdate = false;

            _sceneLoader.LoaderCallback();
        }
    }
}