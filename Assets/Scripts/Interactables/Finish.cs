using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class Finish : MonoBehaviour, IInteractable
    {
        private IGameManager _gameManager;
        
        [Inject]
        private void Construct(IGameManager gameManager)
        {
            this._gameManager = gameManager;
        }

        public void Interact()
        {
            _gameManager.PlayerReachFinish();
        }
    }
}