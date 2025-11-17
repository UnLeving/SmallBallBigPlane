using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class Finish : MonoBehaviour, IInteractable
    {
        private GameManager _gameManager;
        
        [Inject]
        private void Construct(GameManager gameManager)
        {
            this._gameManager = gameManager;
        }

        public void Interact()
        {
            _gameManager.PlayerReachFinish();
        }
    }
}