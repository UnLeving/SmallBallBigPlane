using Reflex.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;

namespace SmallBallBigPlane
{
    public class LevelsManager : MonoBehaviour
    {
        private AssetReferenceGameObject[] _assetReferenceGameObjects;
        private GameObject _currentLevel;
        private AsyncOperationHandle<GameObject> _currentLevelHandle;

        [Inject]
        public void Construct(LevelsSO levelsSO, GameManager gameManager)
        {
            _assetReferenceGameObjects = levelsSO.levelsAssetReferences;
        }
        
        private async void Start()
        {
            await LoadLevelAsync(0);
        }
        
        private async UniTask LoadLevelAsync(int levelIndex)
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
                    Destroy(_currentLevel);
                }

                _currentLevel = null;
            }

            // Instantiate new level under this manager
            _currentLevelHandle = Addressables.InstantiateAsync(_assetReferenceGameObjects[levelIndex], transform);
            _currentLevel = await _currentLevelHandle.Task;
        }
    }
}