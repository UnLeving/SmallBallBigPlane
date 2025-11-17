using Cysharp.Threading.Tasks;
using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.Infrastructure.Services.Factories;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class LooseLevelState : IState
    {
        private StateMachine _stateMachine;
        private readonly WindowsService _windowsService;
        private readonly GameSettingsSO _gameSettings;
        private readonly GameManager _gameManager;
        
        public LooseLevelState(WindowsService windowsService,  GameSettingsSO gameSettings, GameManager gameManager)
        {
            this._windowsService = windowsService;
            this._gameSettings = gameSettings;
            this._gameManager = gameManager;
        }

        public async void Enter()
        {
            _windowsService.Show(WindowId.Lose).Forget();
            
            await UniTask.Delay(_gameSettings.restartDelayMS);
            
            _windowsService.Hide(WindowId.Lose);
            
            _stateMachine.Enter<GameLoopState>();
            
            _gameManager.RestartRequested();
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            this._stateMachine = stateMachine;
        }

        public void Exit()
        {
            
        }
    }
}