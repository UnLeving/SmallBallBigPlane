using Reflex.Attributes;
using SmallBallBigPlane.Collectables;

namespace SmallBallBigPlane
{
    public class GameManager
    {
        [Inject] private CoinManager _coinManager;
        [Inject] private WindowManager _windowManager;

        public event System.Action GameRestarted;
        public event System.Action GameLost;
        public event System.Action GameWon;

        public void PlayerFall()
        {
            _coinManager.ResetCoins();

            GameRestarted?.Invoke();
            
            GameLost?.Invoke();
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