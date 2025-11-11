using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    public interface IWindowManager
    {
        UniTask Show<T>() where T : WindowBase;
        UniTask HideAll();
    }

    public class WindowManager : IWindowManager
    {
        private readonly Dictionary<Type, WindowBase> _windows = new();
        private WindowsSO _windowsSO;
        private RectTransform _windowsParent;

        [Inject]
        public void Construct(WindowsSO windowsSO, RectTransform windowsParent)
        {
            _windowsSO = windowsSO;
            _windowsParent = windowsParent;
        }

        public async UniTask Show<T>() where T : WindowBase
        {
            var type = typeof(T);

            if (!_windows.ContainsKey(type))
            {
                await InstantiateWindow<T>();
            }

            await ((T)_windows[type]).Show();
        }

        public async UniTask HideAll()
        {
            foreach (var window in _windows.Values)
            {
                if (window != null)
                    await window.Hide();
            }
        }

        private async UniTask<T> InstantiateWindow<T>() where T : WindowBase
        {
            var targetType = typeof(T);

            if (_windowsParent == null)
                throw new InvalidOperationException("WindowManager: windows parent is not assigned. Ensure Construct was called with a valid RectTransform.");

            // Find the reference of the requested window type without instantiating unrelated windows
            foreach (var windowRef in _windowsSO.windowsReferences)
            {
                // Load prefab as GameObject (main type) to avoid InvalidKeyException
                var prefab = await windowRef.LoadAssetAsync<GameObject>().ToUniTask();
                var matches = prefab != null && prefab.TryGetComponent<T>(out _);

                // Release loaded asset to avoid holding unnecessary references
                windowRef.ReleaseAsset();

                if (!matches) continue;

                // Instantiate under the provided parent and cache the instance
                var instanceGO = await windowRef.InstantiateAsync(_windowsParent).ToUniTask();
                var instance = instanceGO.GetComponent<WindowBase>();

                if (instance == null)
                    throw new InvalidOperationException($"Prefab for {targetType.Name} does not contain a WindowBase component.");

                instance.gameObject.SetActive(false);

                _windows[targetType] = (WindowBase)instance;
                return (T)instance;
            }

            throw new InvalidOperationException($"Window of type {targetType.Name} was not found in WindowsSO references.");
        }
    }
}