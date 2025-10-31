using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class Finish : MonoBehaviour, IInteractable
    {
        [Inject] private IGameManager _gameManager;
        
        public void Interact()
        {
            _gameManager.PlayerReachFinish();
        }
    }
}