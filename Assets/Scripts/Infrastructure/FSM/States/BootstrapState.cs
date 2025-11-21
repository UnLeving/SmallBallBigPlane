using Cysharp.Threading.Tasks;
using HelpersAndExtensions.SaveSystem;
using SmallBallBigPlane.Collectables;
using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.UI;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class BootstrapState : IState, IInitializable
    {
        private StateMachine _stateMachine;
        private readonly SaveLoadSystem _saveLoadSystem;
        private readonly LoadingScreen _loadingScreen;
        private readonly SceneLoader _sceneLoader;
        private readonly CoinManager _coinManager;

        public BootstrapState(LoadingScreen loadingScreen, SceneLoader sceneLoader, CoinManager coinManager,
            SaveLoadSystem saveLoadSystem)
        {
            this._loadingScreen = loadingScreen;
            this._sceneLoader = sceneLoader;
            this._coinManager = coinManager;
            this._saveLoadSystem = saveLoadSystem;
        }

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

        public async UniTask Initialize()
        {
            _loadingScreen.Show();

            await _saveLoadSystem.TryLoadGame();

            _coinManager.Initialize(_saveLoadSystem.GameData.CoinData);

            _sceneLoader.Load(SceneLoader.Scene.GameScene, () => { _stateMachine.Enter<MainMenuState>(); });
        }
    }
}