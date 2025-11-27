using Cysharp.Threading.Tasks;
using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.Infrastructure.Services.Factories;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class WinLevelState : IState
    {
        private readonly WindowsService _windowsService;
        private readonly LevelsManager _levelsManager;

        public WinLevelState(WindowsService windowsService, LevelsManager levelsManager)
        {
            this._windowsService = windowsService;
            this._levelsManager = levelsManager;
        }

        public void Enter()
        {
            _levelsManager.LevelPassed();
            
            _windowsService.Show(WindowId.Win).Forget();
        }
    }
}