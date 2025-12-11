using SmallBallBigPlane.Collectables;
using SmallBallBigPlane.Infrastructure.Services;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class RestartState: IState, IStateSetStateMachine
    {
        private StateMachine _stateMachine;
        
        private readonly CoinManager _coinManager;
        private readonly GameStateService _gameManager;
        
        public RestartState(CoinManager coinManager, GameStateService gameManager)
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