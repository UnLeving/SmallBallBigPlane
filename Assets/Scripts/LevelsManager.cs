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
        private IGameManager _gameManager;

        [Inject]
        public void Construct(LevelsSO levelsSO, IGameManager gameManager)
        {
            _assetReferenceGameObjects = levelsSO.levelsAssetReferences;
            _gameManager = gameManager;
        }
        
        private async void Start()
        {
            await LoadLevelAsync(0);
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
                    Destroy(_currentLevel);
                }

                _currentLevel = null;
            }

            // Instantiate new level under this manager
            _currentLevelHandle = Addressables.InstantiateAsync(_assetReferenceGameObjects[levelIndex], transform);
            _currentLevel = await _currentLevelHandle.Task;

            // Notify through GameManager that a level is loaded, passing the level root
            _gameManager?.NotifyLevelLoaded(_currentLevel);
        }
    }
}