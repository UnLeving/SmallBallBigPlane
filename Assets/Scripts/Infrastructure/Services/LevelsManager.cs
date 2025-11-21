using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SmallBallBigPlane.Infrastructure.Services
{
    public class LevelsManager : IService
    {
        private AssetReferenceGameObject[] _assetReferenceGameObjects;
        private GameObject _currentLevel;
        private AsyncOperationHandle<GameObject> _currentLevelHandle;

        [Inject]
        public void Construct(LevelsSO levelsSO, GameManager gameManager)
        {
            _assetReferenceGameObjects = levelsSO.levelsAssetReferences;
        }
        
        public async UniTask LoadLevelAsync(int levelIndex)
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
            _currentLevelHandle = Addressables.InstantiateAsync(_assetReferenceGameObjects[levelIndex]);
            _currentLevel = await _currentLevelHandle.Task;
        }
    }
}