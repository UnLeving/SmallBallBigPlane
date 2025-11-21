using Cysharp.Threading.Tasks;
using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.Infrastructure.Services.Factories;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class WinLevelState : IState
    {
        private readonly WindowsService _windowsService;

        public WinLevelState(WindowsService windowsService)
        {
            this._windowsService = windowsService;
        }

        public void Enter()
        {
            _windowsService.Show(WindowId.Win).Forget();
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            
        }

        public void Exit()
        {

        }
    }
}