using Reflex.Attributes;
using SmallBallBigPlane.Collectables;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class Player : MonoBehaviour
    {
        private Vector3 _startPosition;
        [Inject] private GameManager _gameManager;
        
        private void Start()
        {
            _gameManager.GameRestarted += ResetPosition;
            
            _startPosition = transform.position;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }
        
        private void ResetPosition()
        {
            transform.position = _startPosition;
        }
    }
}