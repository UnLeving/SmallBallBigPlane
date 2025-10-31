using System;
using SmallBallBigPlane.Collectables;
using Cysharp.Threading.Tasks;

namespace SmallBallBigPlane
{
    public interface IGameManager
    {
        event Action GameRestarted;
        event Action GameLost;
        event Action GameWon;
        UniTask PlayerFall();
        void PlayerReachFinish();
        void RestartRequested();
    }

    public class GameManager : IGameManager
    {
        private const int RESTART_DELAY_MS = 2000;
        
        private readonly ICoinManager _coinManager;
        private readonly IWindowManager _windowManager;

        public event Action GameRestarted;
        public event Action GameLost;
        public event Action GameWon;

        public GameManager(ICoinManager coinManager, IWindowManager windowManager)
        {
            _coinManager = coinManager;
            _windowManager = windowManager;
        }

        public async UniTask PlayerFall()
        {
            _coinManager.ResetCoins();
            
            GameLost?.Invoke();
            
            _windowManager.Show<LooseWindow>();

            await UniTask.Delay(RESTART_DELAY_MS);

            RestartRequested();
        }

        public void PlayerReachFinish()
        {
            _windowManager.Show<WinLevelWindow>();
            
            GameWon?.Invoke();
        }

        public void RestartRequested()
        {
            _coinManager.ResetCoins();
            
            GameRestarted?.Invoke();
            
            _windowManager.HideAll();
        }
    }
}