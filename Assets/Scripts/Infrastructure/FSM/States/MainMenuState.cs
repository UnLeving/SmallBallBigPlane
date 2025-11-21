using SmallBallBigPlane.UI;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class MainMenuState : IState
    {
        private readonly MainMenu _mainMenu;
        private readonly LoadingScreen _loadingScreen;

        public MainMenuState(MainMenu mainMenu, LoadingScreen loadingScreen)
        {
            this._mainMenu = mainMenu;
            this._loadingScreen = loadingScreen;
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            
        }

        public void Enter()
        {
            _mainMenu.Show();

            _loadingScreen.Hide();
        }

        public void Exit()
        {
            _mainMenu.Hide();
        }
    }
}