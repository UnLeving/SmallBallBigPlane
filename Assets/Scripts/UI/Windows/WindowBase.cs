using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace SmallBallBigPlane
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Transform viewTransform;
        protected bool isOpened;

        public virtual async UniTask Show()
        {
            if (isOpened) return;

            isOpened = true;

            await ShowPanel();
        }

        public virtual async UniTask Hide()
        {
            if (!isOpened) return;

            isOpened = false;

            await HidePanel();
        }

        private async UniTask ShowPanel(CancellationToken cancellationToken = default)
        {
            gameObject.SetActive(true);

            viewTransform.localScale = Vector3.zero;
            
            await Tween.Alpha(_background, 0.5f, 0.5f);

            var tween = Tween.Scale(viewTransform, Vector3.one, 0.5f, Ease.OutBack).ToYieldInstruction();

            await tween.ToUniTask(cancellationToken: cancellationToken);
        }

        private async UniTask HidePanel(CancellationToken cancellationToken = default)
        {
            var tween = Tween.Scale(viewTransform, Vector3.zero, 0.5f, Ease.InBack).ToYieldInstruction();

            await tween.ToUniTask(cancellationToken: cancellationToken);
            
            await Tween.Alpha(_background, 0f, 0.5f);

            gameObject.SetActive(false);
        }
    }
}