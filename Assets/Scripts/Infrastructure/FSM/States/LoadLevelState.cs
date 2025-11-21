using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.UI;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class LoadLevelState : IState
    {
        private StateMachine _stateMachine;
        private readonly LevelsManager _levelsManager;
        private readonly LoadingScreen _loadingScreen;


        public LoadLevelState(LevelsManager  levelsManager, LoadingScreen loadingScreen)
        {
            this._levelsManager = levelsManager;
            this._loadingScreen = loadingScreen;
        }

        public async void Enter()
        {
            _loadingScreen.Show();
            
           await _levelsManager.LoadLevelAsync(0);

           _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        }
        
        public void SetStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}