using Cysharp.Threading.Tasks;
using SmallBallBigPlane.Infrastructure.Services.AssetManagement;
using SmallBallBigPlane.UI.Windows;
using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.Services.Factories
{
    public enum WindowId
    {
        Settings,
        Lose,
        Win,
        MainMenu
    }

    public class UIFactory : IService
    {
        private readonly AssetProvider _assetProvider;

        private Transform _uiRoot;

        public UIFactory(AssetProvider assetProvider)
        {
            this._assetProvider = assetProvider;
        }

        public UniTask<UIContainer> InstantiateRegisteredAsync(WindowId windowId, Transform parent = null)
        {
            return _assetProvider.InstantiateAsync(windowId, parent ?? _uiRoot);
        }

        private UIContainer InstantiateRegistered(WindowId windowId, Transform parent)
        {
            return _assetProvider.Instantiate(windowId, parent);
        }
    }
}