using Cysharp.Threading.Tasks;
using UnityEngine;
using PrimeTween;

namespace SmallBallBigPlane
{
    public class SimpleButton : MonoBehaviour
    {
        [SerializeField] private Transform _animatableParent;
        private Sequence _sequence;

        public void AnimateClick() => AnimateClickAsync().Forget();

        private async UniTask AnimateClickAsync()
        {
            // Stop any previous animation
            if (_sequence.isAlive)
                _sequence.Stop();

            // Create a punch-like scale animation using sequence
            var originalScale = _animatableParent.localScale;
            var punchScale = originalScale + Vector3.one * 0.2f;

            _sequence = Sequence.Create()
                .Chain(Tween.Scale(_animatableParent, punchScale, 0.15f, Ease.OutQuad))
                .Chain(Tween.Scale(_animatableParent, originalScale, 0.15f, Ease.InOutSine));

            await _sequence.ToUniTask();
        }

        private void OnDestroy()
        {
            if (_sequence.isAlive)
                _sequence.Stop();
        }
    }
}