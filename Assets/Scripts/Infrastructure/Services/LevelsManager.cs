using System;
using Cysharp.Threading.Tasks;
using HelpersAndExtensions.SaveSystem;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace SmallBallBigPlane.Infrastructure.Services
{
    [Serializable]
    public class LevelData : ISavable
    {
        [field: SerializeField] public string Id { get; set; }
        [field: SerializeField] public int CurrentLevelIndex { get; set; }
    }

    public class LevelsManager : IService
    {
        public string Id { get; set; } = "Level";

        private AssetReferenceGameObject[] _assetReferenceGameObjects;
        private GameObject _currentLevel;
        private AsyncOperationHandle<GameObject> _currentLevelHandle;
        private LevelData _data;
        private int _currentLevelIndex;

        [Inject]
        public void Construct(LevelsSO levelsSO, GameManager gameManager)
        {
            _assetReferenceGameObjects = levelsSO.levelsAssetReferences;
        }

        public void Initialize(LevelData levelData)
        {
            this._data = levelData;
            this._data.Id = Id;
            this._currentLevelIndex = levelData.CurrentLevelIndex;
        }

        public async UniTask LoadLevelAsync()
        {
            // Unload previous level instance if any
            if (_currentLevel != null)
            {
                if (_currentLevelHandle.IsValid())
                {
                    Addressables.ReleaseInstance(_currentLevelHandle);
                }
                else
                {
                    Object.Destroy(_currentLevel);
                }

                _currentLevel = null;
            }

            // Instantiate new level under this manager
            _currentLevelHandle = Addressables.InstantiateAsync(_assetReferenceGameObjects[_currentLevelIndex]);
            _currentLevel = await _currentLevelHandle.Task;
        }

        public void LevelPassed()
        {
            _currentLevelIndex++;
            if (_currentLevelIndex >= _assetReferenceGameObjects.Length)
            {
                _currentLevelIndex = _assetReferenceGameObjects.Length - 1;
            }
            
            _data.CurrentLevelIndex = _currentLevelIndex;
        }
    }
}