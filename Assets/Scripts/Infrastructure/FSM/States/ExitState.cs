using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class ExitState: IState
    {
        public void Enter()
        {
            Application.Quit();
        }
    }
}