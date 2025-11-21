using Cysharp.Threading.Tasks;
using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.Infrastructure.Services.Factories;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class LooseLevelState : IState, IStateSetStateMachine
    {
        private StateMachine _stateMachine;
        private readonly WindowsService _windowsService;
        private readonly GameSettingsSO _gameSettings;

        
        public LooseLevelState(WindowsService windowsService,  GameSettingsSO gameSettings)
        {
            this._windowsService = windowsService;
            this._gameSettings = gameSettings;
        }

        public async void Enter()
        {
            _windowsService.Show(WindowId.Lose).Forget();
            
            await UniTask.Delay(_gameSettings.restartDelayMS);
            
            _windowsService.Hide(WindowId.Lose);
            
            _stateMachine.Enter<RestartState>();
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            this._stateMachine = stateMachine;
        }
    }
}