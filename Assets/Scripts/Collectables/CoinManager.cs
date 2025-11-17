using System;
using System.Collections.Generic;
using System.Linq;
using HelpersAndExtensions.SaveSystem;
using UnityEngine;

namespace SmallBallBigPlane.Collectables
{
    [Serializable]
    public class CoinData : ISavable
    {
        [field: SerializeField] public string Id { get; set; }
        [field: SerializeField] public int MaxCoinCount { get; set; }
    }
    
    public class CoinManager: IService
    {
        public string Id { get; set; } = "Coin";
        private CoinData data;
        
        private int _coinCount;
        private int _maxCoinCount;
        
        public int CoinCount => _coinCount;
        public int MaxCoinCount => _maxCoinCount;

        public event Action<int> OnCoinCollected;

        private List<Coin> _coins = new();
        
        public void Initialize(CoinData coinData)
        {
            this.data = coinData;
            
            this.data.Id = Id;
            
            this._maxCoinCount = this.data.MaxCoinCount;
            
            var coinsGoList = GameObject.FindGameObjectsWithTag("Coin").ToList();
            
            _coins.Clear();

            foreach (var coinGo in coinsGoList)
            {
                var coin = coinGo.GetComponent<Coin>();
                
                _coins.Add(coin);
            }
            
            ResetCoins();
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

        private void ResetCoins()
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