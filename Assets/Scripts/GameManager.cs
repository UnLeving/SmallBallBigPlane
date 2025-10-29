using Reflex.Attributes;
using SmallBallBigPlane.Collectables;

namespace SmallBallBigPlane
{
    public class GameManager
    {
        [Inject] private CoinManager _coinManager;
        [Inject] private WindowManager _windowManager;

        public event System.Action GameRestarted;

        public void PlayerFall()
        {
            _coinManager.ResetCoins();

            GameRestarted?.Invoke();
        }

        public void PlayerReachFinish()
        {
            _windowManager.Show<WinLevelWindow>();
        }

        public void RestartRequested()
        {
            _coinManager.ResetCoins();
            
            GameRestarted?.Invoke();
            
            _windowManager.HideAll();
        }
    }
}