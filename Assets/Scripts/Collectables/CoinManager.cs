using System;
using System.Collections.Generic;

namespace SmallBallBigPlane.Collectables
{
    public interface ICoinManager
    {
        int CoinCount { get; }
        event Action<int> OnCoinCollected;
        void CollectCoin();
        void ResetCoins();
        void Initialize(IEnumerable<Coin> coins);
    }
    
    public class CoinManager: ICoinManager
    {
        private int _coinCount;
        public int CoinCount => _coinCount;

        public event Action<int> OnCoinCollected;

        private List<Coin> _coins = new();

        public void Initialize(IEnumerable<Coin> coins)
        {
            _coins.AddRange(coins);
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
                if( coin.gameObject.activeSelf) continue;
                
                coin.gameObject.SetActive(true);
            }
        }
    }
}