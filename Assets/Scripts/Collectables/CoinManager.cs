using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using HelpersAndExtensions.SaveSystem;
using Reflex.Attributes;
using SmallBallBigPlane.Effects;
using SmallBallBigPlane.Infrastructure.Services;
using UnityEngine;

namespace SmallBallBigPlane.Collectables
{
    [Serializable]
    public class CoinData : ISavable
    {
        [field: SerializeField] public string Id { get; set; }
        [field: SerializeField] public int[] MaxCoinCount { get; set; }
    }

    public class CoinManager : IService
    {
        public string Id { get; set; } = "Coin";
        private CoinData data;
        private PickupEffectsHandler _pickupEffectsHandler;
        private int _coinCount;
        private int[] _maxCoinCount = new int[2];

        public int CoinCount => _coinCount;
        public int[] MaxCoinCount => _maxCoinCount;
        public int CurrentLevelMaxCoinCount;

        public event Action<int> OnCoinCollected;

        private List<Coin> _coins = new();

        [Inject]
        private void Construct(PickupEffectsHandler pickupEffectsHandler)
        {
            _pickupEffectsHandler = pickupEffectsHandler;
        }

        public UniTask Initialize(CoinData coinData)
        {
            this.data = coinData;

            this.data.Id = Id;

            this._maxCoinCount = this.data.MaxCoinCount;

            var coinsGoList = GameObject.FindGameObjectsWithTag("Coin").ToList();

            if (coinsGoList.Count == 0)
            {
                Debug.LogError("CoinManager.Initialize: coins = " + coinsGoList.Count);
            }

            _coins.Clear();

            foreach (var coinGo in coinsGoList)
            {
                var coin = coinGo.GetComponent<Coin>();

                _coins.Add(coin);
            }

            ResetCoins();

            return UniTask.CompletedTask;
        }

        public void CollectCoin(Coin coin)
        {
            _coinCount++;
            
            _pickupEffectsHandler.PlayPickupEffects(coin.transform.position);

            OnCoinCollected?.Invoke(_coinCount);
        }

        public void SetMaxCoinCount(int ind)
        {
            if (CoinCount < MaxCoinCount[ind]) return;

            _maxCoinCount[ind] = _coinCount;

            CurrentLevelMaxCoinCount = _coinCount;

            data.MaxCoinCount[ind] = _coinCount;
        }

        public void ResetCoins()
        {
            _coinCount = 0;

            OnCoinCollected?.Invoke(0);

            foreach (var coin in _coins)
            {
                coin.Reset();
            }
        }
    }
}