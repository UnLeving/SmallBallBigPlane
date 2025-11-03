using TMPro;
using UnityEngine;
using PrimeTween;

namespace SmallBallBigPlane
{
    public class LoadingSceneHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _loadingText;

        private void Start()
        {
            Tween.Scale(_loadingText.transform, startValue: 1f, endValue: 1.2f, duration: 1.5f, cycles: -1, cycleMode: CycleMode.Yoyo);
            
            Tween.Alpha(_loadingText, startValue: 0.5f, endValue: 1f, duration: 1.5f, cycles: -1, cycleMode: CycleMode.Yoyo);
        }
    }
}