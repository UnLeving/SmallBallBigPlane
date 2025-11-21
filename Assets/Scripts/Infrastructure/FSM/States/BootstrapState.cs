using Cysharp.Threading.Tasks;
using HelpersAndExtensions.SaveSystem;
using SmallBallBigPlane.Collectables;
using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.UI;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class BootstrapState : IState, IStateSetStateMachine ,IInitializable
    {
        private StateMachine _stateMachine;
        private readonly SaveLoadSystem _saveLoadSystem;
        private readonly LoadingScreen _loadingScreen;
        private readonly SceneLoader _sceneLoader;


        public BootstrapState(LoadingScreen loadingScreen, SceneLoader sceneLoader,
            SaveLoadSystem saveLoadSystem)
        {
            this._loadingScreen = loadingScreen;
            this._sceneLoader = sceneLoader;
            this._saveLoadSystem = saveLoadSystem;
        }

        public void Enter()
        {
            Initialize().Forget();
        }
        
        public void SetStateMachine(StateMachine value)
        {
            _stateMachine = value;
        }

        public async UniTask Initialize()
        {
            _loadingScreen.Show();

            await _saveLoadSystem.TryLoadGame();

            _sceneLoader.Load(SceneLoader.Scene.GameScene, () => { _stateMachine.Enter<MainMenuState>(); });
        }
    }
}