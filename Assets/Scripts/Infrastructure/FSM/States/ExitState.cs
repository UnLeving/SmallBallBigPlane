using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class ExitState: IState
    {
        public void Enter()
        {
            Application.Quit();
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            
        }

        public void Exit()
        {
            
        }
    }
}