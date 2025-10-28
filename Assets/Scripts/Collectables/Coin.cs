using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane.Collectables
{
    public class Coin : MonoBehaviour, ICollectable
    {
       [Inject] private CoinManager _coinManager;
        
        public void Collect()
        {
            _coinManager.CollectCoin();
            
            gameObject.SetActive(false);
        }
    }
}