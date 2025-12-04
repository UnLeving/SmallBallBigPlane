using System.Collections;
using Cysharp.Threading.Tasks;
using PrimeTween;
using UnityEngine;

namespace SmallBallBigPlane.UI
{
    public sealed class SimpleButton : MonoBehaviour
    {
        [SerializeField] private Transform _animatableParent;
        private IEnumerator _sequence;
        private bool _animating;

        public void AnimateClick()
        {
            AnimateClickAsync().Forget();
        }

        private async UniTask AnimateClickAsync()
        {
            if (_animating) return;

            _animating = true;

            var originalScale = _animatableParent.localScale;
            var punchScale = originalScale + Vector3.one * 0.2f;

            _sequence = Sequence.Create()
                .Chain(Tween.Scale(_animatableParent, punchScale, 0.15f, Ease.OutQuad))
                .Chain(Tween.Scale(_animatableParent, originalScale, 0.15f, Ease.InOutSine))
                .ToYieldInstruction();

            await _sequence.ToUniTask();

            _animating = false;
        }
    }
}