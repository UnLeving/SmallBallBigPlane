namespace SmallBallBigPlane.Infrastructure.FSM
{
    public interface IState
    {
        void Enter();
    }
    
    public interface IStateExitable
    {
        void Exit();
    }
    
    public interface IStateSetStateMachine
    {
        void SetStateMachine(StateMachine stateMachine);
    }
}