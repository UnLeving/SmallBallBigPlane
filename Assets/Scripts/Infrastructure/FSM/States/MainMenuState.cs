using Cysharp.Threading.Tasks;
using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.Infrastructure.Services.Factories;
using SmallBallBigPlane.UI;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class MainMenuState : IState, IStateExitable
    {
        private readonly WindowsService _windowsService;
        private readonly LoadingScreen _loadingScreen;

        public MainMenuState(WindowsService windowsService, LoadingScreen loadingScreen)
        {
            this._windowsService = windowsService;
            this._loadingScreen = loadingScreen;
        }
        
        public void Enter()
        {
            _windowsService.Show(WindowId.MainMenu).Forget();

            _loadingScreen.Hide();
        }

        public void Exit()
        {
            _windowsService.Hide(WindowId.MainMenu);
        }
    }
}