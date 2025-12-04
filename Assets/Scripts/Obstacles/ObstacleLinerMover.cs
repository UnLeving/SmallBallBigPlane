using System;
using UnityEngine;
using PrimeTween;
using Cysharp.Threading.Tasks;

namespace SmallBallBigPlane.Obstacles
{
    public class ObstacleLinerMover : MonoBehaviour
    {
        [SerializeField] private Transform transformToMove;
        [SerializeField] private Transform pointA;
        [SerializeField] private Transform pointB;
        [SerializeField] private int delayOnPointMS = 1000;
        [SerializeField] private float moveDurationSec = 1f;

        private Tween _currentTween;
        private bool _isRunning;

        private void Awake()
        {
            pointA.gameObject.SetActive(false);
            pointB.gameObject.SetActive(false);
        }

        private void Start()
        {
            _isRunning = true;
            
            MoveLoop().Forget();
        }

        private void OnDisable()
        {
            _isRunning = false;
            
            _currentTween.Stop();
        }

        private async UniTaskVoid MoveLoop()
        {
            transformToMove.position = pointA.position;

            while (_isRunning)
            {
                // A → B
                await MoveTo(pointB.position);

                // pause
                await UniTask.Delay(delayOnPointMS, cancellationToken: this.GetCancellationTokenOnDestroy());

                // B → A
                await MoveTo(pointA.position);

                // pause
                await UniTask.Delay(delayOnPointMS, cancellationToken: this.GetCancellationTokenOnDestroy());
            }
        }

        private async UniTask MoveTo(Vector3 targetPos)
        {
            _currentTween = Tween.Position(
                target: transformToMove,
                endValue: targetPos,
                duration: moveDurationSec,
                Ease.InOutQuad
            );

            await _currentTween;
        }
    }
}