using Cysharp.Threading.Tasks;
using SmallBallBigPlane.Collectables;
using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.Infrastructure.Services.Factories;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class WinLevelState : IState
    {
        private readonly WindowsService _windowsService;
        private readonly LevelsManager _levelsManager;
        private readonly CoinManager _coinManager;

        public WinLevelState(WindowsService windowsService, LevelsManager levelsManager, CoinManager coinManager)
        {
            this._windowsService = windowsService;
            this._levelsManager = levelsManager;
            this._coinManager = coinManager;
        }

        public void Enter()
        {
            _coinManager.SetMaxCoinCount(_levelsManager.CurrentLevelIndex);

            _windowsService.Show(WindowId.Win).Forget();
        }
    }
}