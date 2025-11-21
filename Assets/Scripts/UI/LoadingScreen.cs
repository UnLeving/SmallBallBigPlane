using System.Collections;
using Cysharp.Threading.Tasks;
using PrimeTween;
using SmallBallBigPlane.UI.Windows;
using TMPro;
using UnityEngine;

namespace SmallBallBigPlane.UI
{
    public class LoadingScreen : UIContainer
    {
        [SerializeField] private TextMeshProUGUI loadingText;
        [SerializeField] private CanvasGroup canvasGroup;

        // todo: move to SO settings
        private readonly float _fadeSpeed = 2.35f;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            Tween.Scale(loadingText.transform, startValue: 1f, endValue: 1.2f, duration: 1.5f, cycles: -1,
                cycleMode: CycleMode.Yoyo);

            Tween.Alpha(loadingText, startValue: 0.5f, endValue: 1f, duration: 1.5f, cycles: -1,
                cycleMode: CycleMode.Yoyo);
        }
        
        public override UniTask Show()
        {
            canvasGroup.alpha = 1;
            gameObject.SetActive(true);
            
            return UniTask.CompletedTask;
        }

        public override UniTask Hide()
        {
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(FadeIn());
            }
            
            return UniTask.CompletedTask;
        }

        private IEnumerator FadeIn()
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * _fadeSpeed;
                
                yield return null;
            }

            canvasGroup.alpha = 0;
            
            gameObject.SetActive(false);
        }
    }
}