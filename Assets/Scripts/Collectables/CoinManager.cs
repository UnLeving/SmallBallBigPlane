using System;
using System.Collections.Generic;
using UnityEngine;

namespace SmallBallBigPlane.Collectables
{
    public class CoinManager
    {
        private int _coinCount;
        public int CoinCount => _coinCount;

        public event Action<int> OnCoinCollected;

        private List<GameObject> _coins = new();
        public CoinManager()
        {
            _coins.AddRange(GameObject.FindGameObjectsWithTag("Coin"));
        }
        
        public void CollectCoin()
        {
            _coinCount++;
            
            OnCoinCollected?.Invoke(_coinCount);
        }

        public void ResetCoins()
        {
            _coinCount = 0;
            
            OnCoinCollected?.Invoke(_coinCount);

            foreach (var coin in _coins)
            {
                if( coin.activeSelf) continue;
                
                coin.SetActive(true);
            }
        }
    }
}