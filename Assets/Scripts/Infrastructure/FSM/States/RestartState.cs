using SmallBallBigPlane.Collectables;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class RestartState: IState, IStateSetStateMachine
    {
        private StateMachine _stateMachine;
        
        private readonly CoinManager _coinManager;
        private readonly GameManager _gameManager;
        
        public RestartState(CoinManager coinManager, GameManager gameManager)
        {
            this._coinManager = coinManager;
            this._gameManager = gameManager;
        }
        
        public void Enter()
        {
            _coinManager.ResetCoins();
            
            _stateMachine.Enter<GameLoopState>();
            
            _gameManager.RestartRequested();
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            this._stateMachine = stateMachine;
        }
    }
}