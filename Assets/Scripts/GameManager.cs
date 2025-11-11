using System;
using SmallBallBigPlane.Collectables;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SmallBallBigPlane
{
    public interface IGameManager
    {
        event Action GameRestarted;
        event Action GameLost;
        event Action GameWon;
        event Action<GameObject> OnLevelLoaded;
        UniTask PlayerFall();
        UniTask PlayerReachFinish();
        void RestartRequested();
        void NotifyLevelLoaded(GameObject levelRoot);
    }

    public class GameManager : IGameManager
    {
        private const int RESTART_DELAY_MS = 2500;

        private readonly ICoinManager _coinManager;
        private readonly IWindowManager _windowManager;

        public event Action GameRestarted;
        public event Action GameLost;
        public event Action GameWon;
        public event Action<GameObject> OnLevelLoaded;

        public GameManager(ICoinManager coinManager, IWindowManager windowManager)
        {
            _coinManager = coinManager;
            _windowManager = windowManager;
        }

        public void NotifyLevelLoaded(GameObject levelRoot)
        {
            OnLevelLoaded?.Invoke(levelRoot);
        }

        public async UniTask PlayerFall()
        {
            _coinManager.ResetCoins();

            GameLost?.Invoke();

            await _windowManager.Show<LooseWindow>();

            await UniTask.Delay(RESTART_DELAY_MS);

            RestartRequested();
        }

        public async UniTask PlayerReachFinish()
        {
            GameWon?.Invoke();
            
            await _windowManager.Show<WinLevelWindow>();
        }

        public void RestartRequested()
        {
            _coinManager.ResetCoins();

            GameRestarted?.Invoke();

            _windowManager.HideAll();
        }
    }
}