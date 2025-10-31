using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane.Collectables
{
    public class Coin : MonoBehaviour, ICollectable
    {
       [SerializeField] private float rotationSpeed = 10f;
       [Inject] private ICoinManager _coinManager;
       [Inject] private IPickupEffectsHandler _pickupEffectsHandler;
       
        public void Collect()
        {
            _coinManager.CollectCoin();
            
            gameObject.SetActive(false);
            
            _pickupEffectsHandler.PlayPickupEffects(transform.position);
        }

        public void Reset()
        {
            if(gameObject.activeSelf == true) return;
            
            gameObject.SetActive(true);
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}