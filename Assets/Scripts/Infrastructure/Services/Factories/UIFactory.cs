using Cysharp.Threading.Tasks;
using SmallBallBigPlane.Infrastructure.Services.AssetManagement;
using SmallBallBigPlane.UI.Windows;
using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.Services.Factories
{
    public enum WindowId
    {
        Unknown,
        Pause,
        Leaderboard,
        Lose,
        Win,
        Tutorial,
        HUD
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
        
        private UniTask<UIContainer> InstantiateRegistered(WindowId windowId, Transform parent)
        {
           return _assetProvider.Instantiate(windowId, parent);
        }
        
        public async UniTask<UIContainer> CreateWinWindow()
        {
           return await InstantiateRegistered(WindowId.Win, _uiRoot);
        }
        
        public async UniTask<UIContainer> CreateLoseWindow()
        {
            return await InstantiateRegistered(WindowId.Lose, _uiRoot);
        }
    }
}