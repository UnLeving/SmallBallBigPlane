using System;
using System.Collections.Generic;
using HelpersAndExtensions.SaveSystem;
using UnityEngine;

namespace SmallBallBigPlane.Collectables
{
    public interface ICoinManager
    {
        int CoinCount { get; }
        int MaxCoinCount { get; }
        event Action<int> OnCoinCollected;
        void CollectCoin();
        void ResetCoins();
        void Initialize(IEnumerable<Coin> coins);
        void SetMaxCoinCount();
    }

    [Serializable]
    public class CoinData : ISavable
    {
        [field: SerializeField] public string Id { get; set; }
        [field: SerializeField] public int MaxCoinCount { get; set; }
    }
    
    public class CoinManager : MonoBehaviour, ICoinManager, IBind<CoinData>
    {
        [field: SerializeField] public string Id { get; set; }
        [SerializeField] private CoinData data;
        
        private int _coinCount;
        private int _maxCoinCount;
        
        public int CoinCount => _coinCount;
        public int MaxCoinCount => _maxCoinCount;

        public event Action<int> OnCoinCollected;

        private List<Coin> _coins = new();

        public void Bind(CoinData data)
        {
            this.data = data;
            this.data.Id = Id;
            
            Debug.Log($"CoinManager: {data.MaxCoinCount}");
            
            this._maxCoinCount = data.MaxCoinCount;
        }
        
        public void Initialize(IEnumerable<Coin> coins)
        {
            _coins.AddRange(coins);
        }

        public void CollectCoin()
        {
            _coinCount++;

            OnCoinCollected?.Invoke(_coinCount);
        }

        public void SetMaxCoinCount()
        {
            if (CoinCount < MaxCoinCount) return;
            
            _maxCoinCount = _coinCount;
            
            data.MaxCoinCount = _maxCoinCount;
        }

        public void ResetCoins()
        {
            _coinCount = 0;

            OnCoinCollected?.Invoke(_coinCount);

            foreach (var coin in _coins)
            {
                coin.Reset();
            }
        }
    }
}