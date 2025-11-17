using Cysharp.Threading.Tasks;
using SmallBallBigPlane.Infrastructure.FSM;

namespace SmallBallBigPlane
{
    public interface IState
    {
        void Enter();
        void SetStateMachine(StateMachine stateMachine);
        void Exit();
    }
}