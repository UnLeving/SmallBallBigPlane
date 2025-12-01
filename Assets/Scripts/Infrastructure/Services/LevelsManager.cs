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
        public int CurrentLevelIndex { get; private set; }

        [Inject]
        public void Construct(LevelsSO levelsSO)
        {
            _assetReferenceGameObjects = levelsSO.levelsAssetReferences;
        }

        public void Initialize(LevelData levelData)
        {
            this._data = levelData;
            this._data.Id = Id;
            this.CurrentLevelIndex = levelData.CurrentLevelIndex;
        }

        public async UniTask<int> LoadLevelAsync()
        {
            // Unload previous level instance if any
            if (_currentLevel)
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

            _currentLevelHandle = Addressables.InstantiateAsync(_assetReferenceGameObjects[CurrentLevelIndex]);
            _currentLevel = await _currentLevelHandle.Task;
            
            return CurrentLevelIndex;
        }

        public void LevelPassed()
        {
            CurrentLevelIndex++;
            if (CurrentLevelIndex >= _assetReferenceGameObjects.Length)
            {
                CurrentLevelIndex = 0;
            }
            
            _data.CurrentLevelIndex = CurrentLevelIndex;
        }
    }
}