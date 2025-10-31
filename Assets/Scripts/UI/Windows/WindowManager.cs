using System;
using System.Collections.Generic;
using UnityEngine;

namespace SmallBallBigPlane
{
    public interface IWindowManager
    {
        T Get<T>() where T : WindowBase;
        void Show<T>() where T : WindowBase;
        void HideAll();
        void Initialize(IEnumerable<WindowBase> windows, GameObject background);
    }

    public class WindowManager : IWindowManager
    {
        private Dictionary<Type, WindowBase> _windows = new();
        private GameObject _background;

        public void Initialize(IEnumerable<WindowBase> windows, GameObject background)
        {
            foreach (var window in windows)
            {
                _windows.Add(window.GetType(), window);
                window.Hide();
            }

            _background = background;

            _background.SetActive(false);
        }

        public T Get<T>() where T : WindowBase
        {
            return (T)_windows[typeof(T)];
        }

        public void Show<T>() where T : WindowBase
        {
            foreach (var kvp in _windows)
                kvp.Value.Hide();

            Get<T>().Show();

            _background.SetActive(true);
        }

        public void HideAll()
        {
            foreach (var window in _windows.Values)
                window.Hide();

            _background.SetActive(false);
        }
    }
}