using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using SmallBallBigPlane.Infrastructure.Services;
using UnityEngine;

namespace SmallBallBigPlane.Interactables
{
    public class Finish : MonoBehaviour, IInteractable
    {
        [SerializeField] private ParticleSystem particles;
        
        private GameStateService _gameManager;
        
        [Inject]
        private void Construct(GameStateService gameManager)
        {
            this._gameManager = gameManager;
        }

        public async UniTask Interact()
        {
            particles.Play();
            
            while (particles.isPlaying)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            
            _gameManager.PlayerReachFinish();
        }
    }
}