using Reflex.Attributes;
using Reflex.Core;
using SmallBallBigPlane.Infrastructure.FSM;
using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.DI.Installers
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Container _container;

        [Inject]
        private void Construct(Container container)
        {
            _container = container;
            
            StartGame();
        }
        

        private void StartGame()
        {
            var s = _container.Resolve<StateMachine>();
            
            s.Initialize();
        }
    }
}