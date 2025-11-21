using Cysharp.Threading.Tasks;
using SmallBallBigPlane.Infrastructure.Services.AssetManagement;
using SmallBallBigPlane.UI;
using SmallBallBigPlane.UI.Windows;
using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.Services.Factories
{
    public enum WindowId
    {
        Unknown,
        MainMenu,
        Settings,
        LoadingScreen,
        Lose,
        Win
    }

    public class UIFactory : IService
    {
        private readonly AssetProvider _assetProvider;

        private Transform _uiRoot;

        public UIFactory(AssetProvider assetProvider)
        {
            this._assetProvider = assetProvider;

            Initialize().Forget();
        }

        private async UniTask Initialize()
        {
            _uiRoot = (await _assetProvider.CreateUIRoot()).transform;
        }

        public Transform GetUIRoot()
        {
            return _uiRoot;
        }

        private UniTask<UIContainer> InstantiateRegisteredAsync(WindowId windowId, Transform parent)
        {
            return _assetProvider.InstantiateAsync(windowId, parent);
        }
        
        private UIContainer InstantiateRegistered(WindowId windowId, Transform parent)
        {
            return _assetProvider.Instantiate(windowId, parent);
        }

        public async UniTask<UIContainer> CreateWinWindow()
        {
            return await InstantiateRegisteredAsync(WindowId.Win, _uiRoot);
        }

        public async UniTask<UIContainer> CreateLoseWindow()
        {
            return await InstantiateRegisteredAsync(WindowId.Lose, _uiRoot);
        }
    }
}