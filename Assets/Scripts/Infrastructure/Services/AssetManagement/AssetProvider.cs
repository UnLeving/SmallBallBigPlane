using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Reflex.Core;
using Reflex.Injectors;
using SmallBallBigPlane.Infrastructure.Services.Factories;
using SmallBallBigPlane.UI;
using SmallBallBigPlane.UI.Windows;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SmallBallBigPlane.Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IService
    {
        private readonly Container _container;
        private readonly WindowsSO _windowsSo;

        public AssetProvider(Container container, WindowsSO windowsSo)
        {
            this._container = container;
            this._windowsSo = windowsSo;
        }

        public UniTask<UIRoot> CreateUIRoot()
        {
            return InstantiateAsync<UIRoot>(AssetPath.UI_ROOT_PATH);
        }

        private UniTask<T> LoadAsync<T>(string path) where T : Object
        {
            var asset = Addressables.LoadAssetAsync<T>(path);

            return asset.ToUniTask();
        }

        public T Instantiate<T>(GameObject prefab, Vector3 position = default, Quaternion rotation = default,
            Transform parent = null, bool isActivateGameObject = true, bool componentEnabled = true, bool inject = true)
            where T : MonoBehaviour
        {
            prefab.SetActive(false);
            GameObject gameObject = Object.Instantiate(prefab, position, rotation, parent);
            T component = gameObject.GetComponent<T>();

            if (inject)
            {
                GameObjectInjector.InjectSingle(gameObject, _container.Parent);
            }

            component.enabled = componentEnabled;
            gameObject.SetActive(isActivateGameObject);
            prefab.SetActive(isActivateGameObject);

            return component;
        }

        private async UniTask<T> InstantiateAsync<T>(string path, Vector3 position = default,
            Quaternion rotation = default,
            Transform parent = null, bool overrideTransform = true, bool isActivateGameObject = true,
            bool componentEnabled = true)
            where T : MonoBehaviour
        {
            var prefab = await LoadAsync<GameObject>(path);

            if (overrideTransform == false)
            {
                position = prefab.transform.position;
                rotation = prefab.transform.rotation;
            }

            return Instantiate<T>(prefab, position, rotation, parent, isActivateGameObject, componentEnabled);
        }

        public async UniTask<UIContainer> InstantiateAsync(WindowId windowId, Transform uiRoot)
        {
            var prefabRef = _windowsSo.GetAssetReferenceByWindowsId(windowId);

            AsyncOperationHandle<GameObject> asyncOperationHandle = prefabRef.LoadAssetAsync<GameObject>();
            UniTask<GameObject> task = asyncOperationHandle.ToUniTask();

            var prefab = await task;

            var component = Instantiate<UIContainer>(prefab, parent: uiRoot);

            component.gameObject.SetActive(true);

            return component;
        }

        public UIContainer Instantiate(WindowId windowId, Transform uiRoot)
        {
            var prefabRef = _windowsSo.GetAssetReferenceByWindowsId(windowId);

            AsyncOperationHandle<GameObject> asyncOperationHandle = prefabRef.LoadAssetAsync<GameObject>();

            var component = Instantiate<UIContainer>(asyncOperationHandle.Result, parent: uiRoot, inject: false);

            component.gameObject.SetActive(true);

            return component;
        }
    }
}