using System;
using UnityEngine;

namespace SmallBallBigPlane.Collectables
{
    public class CoinManager
    {
        private int _coinCount;
        public int CoinCount => _coinCount;

        public event Action<int> OnCoinCollected;

        public void CollectCoin()
        {
            _coinCount++;
            
            OnCoinCollected?.Invoke(_coinCount);
            
            Debug.Log("Collected Coin: " + _coinCount);
        }

        public void ResetCoins()
        {
            _coinCount = 0;
            
            OnCoinCollected?.Invoke(_coinCount);
        }
    }
}