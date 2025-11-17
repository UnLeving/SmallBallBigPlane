using TMPro;
using UnityEngine;
using PrimeTween;
using Reflex.Attributes;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using SmallBallBigPlane.Infrastructure.Services;

namespace SmallBallBigPlane
{
    public class LoadingSceneHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI loadingText;
        [SerializeField] private Slider loadingSlider;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            this._sceneLoader = sceneLoader;
        }

        private void Start()
        {
            Tween.Scale(loadingText.transform, startValue: 1f, endValue: 1.2f, duration: 1.5f, cycles: -1,
                cycleMode: CycleMode.Yoyo);

            Tween.Alpha(loadingText, startValue: 0.5f, endValue: 1f, duration: 1.5f, cycles: -1,
                cycleMode: CycleMode.Yoyo);

            ResetLoadingSliderValue();

            AsynchronousLoad().Forget();
        }

        private void ResetLoadingSliderValue()
        {
            loadingSlider.value = 0f;
        }

        private async UniTaskVoid AsynchronousLoad()
        {
            await UniTask.Yield();

            AsyncOperation ao = _sceneLoader.LoadSceneAsync(SceneLoader.Scene.GameScene);

            while (!ao.isDone)
            {
                float progress = Mathf.Clamp01(ao.progress / 0.9f);

                loadingSlider.value = progress;

                await UniTask.Yield();
            }
        }

        // use allowSceneActivation if need player to interact with scene ex: tap to play
        private async UniTaskVoid AsynchronousLoadWithAllowSceneActivation()
        {
            await UniTask.Yield();

            AsyncOperation ao = _sceneLoader.LoadSceneAsync(SceneLoader.Scene.GameScene);
            ao.allowSceneActivation = false;

            while (!ao.isDone)
            {
                // [0, 0.9] &gt; [0, 1]
                float progress = Mathf.Clamp01(ao.progress / 0.9f);
                Debug.Log("Loading progress: " + (progress * 100) + "%");

                loadingSlider.value = progress;

                // todo implement new input system
                // Loading completed
                // if (ao.progress == 0.9f)
                // {
                //     Debug.Log("Press a key to start");
                //     if (Input.AnyKey())
                //         ao.allowSceneActivation = true;
                // }

                await UniTask.Yield();
            }
        }
    }
}