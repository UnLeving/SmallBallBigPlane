using Reflex.Attributes;
using SmallBallBigPlane.Collectables;

namespace SmallBallBigPlane
{
    public class GameManager
    {
        [Inject] private CoinManager _coinManager;

        public event System.Action GameRestarted;
        
        public void PlayerFall()
        {
            _coinManager.ResetCoins();

            GameRestarted?.Invoke();
        }
    }
}