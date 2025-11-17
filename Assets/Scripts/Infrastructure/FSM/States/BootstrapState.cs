using Cysharp.Threading.Tasks;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class BootstrapState : IState, IInitializable
    {
        private StateMachine _stateMachine;
        
        public void Enter()
        {
            Initialize().Forget();
        }

        public void Exit()
        {
        }

        public void SetStateMachine(StateMachine value)
        {
            _stateMachine = value;
        }

        public UniTask Initialize()
        {
            _stateMachine.Enter<LoadGameState>();

            return UniTask.CompletedTask;
        }
    }
}