using System;
using System.Collections.Generic;
using System.Linq;
using HelpersAndExtensions.SaveSystem;
using UnityEngine;
using Reflex.Attributes;

namespace SmallBallBigPlane.Collectables
{
    public interface ICoinManager
    {
        int CoinCount { get; }
        int MaxCoinCount { get; }
        event Action<int> OnCoinCollected;
        void CollectCoin();
        void ResetCoins();
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
        private IGameManager _gameManager;

        [Inject]
        private void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
            _gameManager.OnLevelLoaded += HandleLevelLoaded;
        }

        private void OnDestroy()
        {
            if (_gameManager != null)
            {
                _gameManager.OnLevelLoaded -= HandleLevelLoaded;
            }
        }

        private void HandleLevelLoaded(GameObject levelRoot)
        {
            var coins = levelRoot.GetComponentsInChildren<Coin>(true);

            Initialize(coins);
            
            ResetCoins();
        }

        public void Bind(CoinData data)
        {
            this.data = data;
            this.data.Id = Id;
            
            this._maxCoinCount = data.MaxCoinCount;
        }
        
        private void Initialize(IEnumerable<Coin> coins)
        {
            var list = coins as IList<Coin> ?? coins.ToList();
            
            _coins.Clear();
            _coins.AddRange(list);
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