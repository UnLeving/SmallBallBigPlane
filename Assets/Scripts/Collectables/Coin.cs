using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane.Collectables
{
    public class Coin : MonoBehaviour, ICollectable
    {
       [Inject] private CoinManager _coinManager;
       [SerializeField] private float rotationSpeed = 10f;
       
        public void Collect()
        {
            _coinManager.CollectCoin();
            
            gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}