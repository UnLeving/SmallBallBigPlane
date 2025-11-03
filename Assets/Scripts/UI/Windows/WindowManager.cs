using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace SmallBallBigPlane
{
    public interface IWindowManager
    {
        T Get<T>() where T : WindowBase;
        UniTask Show<T>() where T : WindowBase;
        void HideAll();
        void Initialize(IEnumerable<WindowBase> windows);
    }

    public class WindowManager : IWindowManager
    {
        private Dictionary<Type, WindowBase> _windows = new();

        public void Initialize(IEnumerable<WindowBase> windows)
        {
            foreach (var window in windows)
            {
                _windows.Add(window.GetType(), window);
            }
        }

        public T Get<T>() where T : WindowBase
        {
            return (T)_windows[typeof(T)];
        }

        public async UniTask Show<T>() where T : WindowBase
        {
            await Get<T>().Show();
        }

        public void HideAll()
        {
            foreach (var window in _windows.Values)
                window.Hide();
        }
    }
}