using SmallBallBigPlane.UI;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class GameLoopState : IState, IStateExitable, IStateSetStateMachine
    {
        private StateMachine _stateMachine;
        private readonly GameManager _gameManager;
        private readonly LoadingScreen _loadingScreen;
        
        public GameLoopState(GameManager gameManager, LoadingScreen loadingScreen)
        {
            this._gameManager = gameManager;
            this._loadingScreen = loadingScreen;
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _gameManager.GameWon += GameManager_OnGameWon;
            _gameManager.GameLost += GameManager_OnGameLost;
            
            _loadingScreen.Hide();
        }

        private void GameManager_OnGameLost()
        {
            _stateMachine.Enter<LooseLevelState>();
        }

        private void GameManager_OnGameWon()
        {
            _stateMachine.Enter<WinLevelState>();
        }

        public void Exit()
        {
            _gameManager.GameWon -= GameManager_OnGameWon;
            _gameManager.GameLost -= GameManager_OnGameLost;
        }
    }
}