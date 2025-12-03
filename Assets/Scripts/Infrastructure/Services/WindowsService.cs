using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SmallBallBigPlane.Infrastructure.Services.Factories;
using SmallBallBigPlane.UI.Windows;

namespace SmallBallBigPlane.Infrastructure.Services
{
    public class WindowsService : IService
    {
        private readonly UIFactory _uiFactory;
        private readonly Dictionary<WindowId, UIContainer> _instantiatedWindows = new();

        public WindowsService(UIFactory uiFactory)
        {
            this._uiFactory = uiFactory;
        }

        public async UniTask<UIContainer> Show(WindowId windowId)
        {
            if (!_instantiatedWindows.ContainsKey(windowId))
            {
                UIContainer window = await _uiFactory.InstantiateRegisteredAsync(windowId);

                if (window == null)
                {
                    UnityEngine.Debug.LogError(
                        $"WindowsService.Show: There is no window component on {windowId} window");
                }

                _instantiatedWindows[windowId] = window;
            }

            UIContainer windowBase = _instantiatedWindows[windowId];

            windowBase.Show().Forget();

            return windowBase;
        }

        public void Hide(WindowId windowId)
        {
            if (_instantiatedWindows.TryGetValue(windowId, out var windowBase))
            {
                windowBase.Hide();
            }
            else
            {
                UnityEngine.Debug.LogError("WindowsService.Hide: No window found");
            }
        }
    }
}