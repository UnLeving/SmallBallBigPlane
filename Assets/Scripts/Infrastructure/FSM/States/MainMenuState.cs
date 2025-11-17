using SmallBallBigPlane.Infrastructure.Services;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class MainMenuState : IState
    {
        private StateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public MainMenuState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _sceneLoader.LoadSceneAsync(SceneLoader.Scene.MainMenuScene);
        }

        public void Exit()
        {
        }
    }
}