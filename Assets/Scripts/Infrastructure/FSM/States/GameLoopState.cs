namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class GameLoopState : IState
    {
        private StateMachine _stateMachine;
        private readonly GameManager _gameManager;
        
        public GameLoopState(GameManager gameManager)
        {
            this._gameManager = gameManager;
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _gameManager.GameWon += GameManager_OnGameWon;
            _gameManager.GameLost += GameManager_OnGameLost;
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