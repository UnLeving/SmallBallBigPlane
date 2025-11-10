using Reflex.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SmallBallBigPlane
{
    public class LevelsManager : MonoBehaviour
    {
        private AssetReferenceGameObject[] _assetReferenceGameObjects;
        private GameObject _currentLevel;

        [Inject]
        public void Construct(LevelsSO levelsSO)
        {
            this._assetReferenceGameObjects = levelsSO.levelsAssetReferences;
        }
        
        private void Start()
        {
            LoadLevel(0);
        }
        
        private void LoadLevel(int levelIndex)
        {
            AsyncOperationHandle<GameObject> ao = Addressables.InstantiateAsync(_assetReferenceGameObjects[levelIndex], transform);
        }
    }
}