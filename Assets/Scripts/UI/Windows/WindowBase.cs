using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace SmallBallBigPlane.UI.Windows
{
    public abstract class WindowBase : UIContainer
    {
        [SerializeField] private Image _background;
        [SerializeField] private Transform viewTransform;
        protected bool isOpened;
        
        protected async UniTask ShowPanel(CancellationToken cancellationToken = default)
        {
            gameObject.SetActive(true);

            // Reset state
            viewTransform.localScale = Vector3.zero;
            var color = _background.color;
            color.a = 0f;
            _background.color = color;

            // Run both animations in parallel
            await UniTask.WhenAll(
                Tween.Alpha(_background, 0.5f, 0.5f).ToUniTask(cancellationToken: cancellationToken),
                Tween.Scale(viewTransform, Vector3.one, 0.5f, Ease.OutBack).ToUniTask(cancellationToken: cancellationToken)
            );
        }

        protected async UniTask HidePanel(CancellationToken cancellationToken = default)
        {
            // Run both in parallel
            await UniTask.WhenAll(
                Tween.Scale(viewTransform, Vector3.zero, 0.5f, Ease.InBack).ToUniTask(cancellationToken: cancellationToken),
                Tween.Alpha(_background, 0f, 0.5f).ToUniTask(cancellationToken: cancellationToken)
            );

            gameObject.SetActive(false);
        }
    }
}