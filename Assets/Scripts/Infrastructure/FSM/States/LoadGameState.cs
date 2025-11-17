using Cysharp.Threading.Tasks;
using HelpersAndExtensions.SaveSystem;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class LoadGameState : IState
    {
        private readonly SaveLoadSystem _saveLoadSystem;
        private StateMachine _stateMachine;

        public LoadGameState(SaveLoadSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
        }

        public void Enter()
        {
            LoadProgress().Forget();
        }

        public void Exit()
        {
        }

        private async UniTask LoadProgress()
        {
            await _saveLoadSystem.TryLoadGame();
            
            _stateMachine.Enter<MainMenuState>();
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}