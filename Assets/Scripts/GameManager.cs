using Reflex.Attributes;
using SmallBallBigPlane.Collectables;
using Cysharp.Threading.Tasks;

namespace SmallBallBigPlane
{
    public class GameManager
    {
        private const int RESTART_DELAY_MS = 2000;
        
        [Inject] private CoinManager _coinManager;
        [Inject] private WindowManager _windowManager;

        public event System.Action GameRestarted;
        public event System.Action GameLost;
        public event System.Action GameWon;

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