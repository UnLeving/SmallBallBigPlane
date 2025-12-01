using HelpersAndExtensions.SaveSystem;
using SmallBallBigPlane.Collectables;
using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.UI;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class LoadLevelState : IState, IStateExitable, IStateSetStateMachine
    {
        private StateMachine _stateMachine;
        private readonly LevelsManager _levelsManager;
        private readonly LoadingScreen _loadingScreen;
        private readonly CoinManager _coinManager;
        private readonly SaveLoadSystem _saveLoadSystem;

        public LoadLevelState(LevelsManager levelsManager, LoadingScreen loadingScreen, CoinManager coinManager,
            SaveLoadSystem saveLoadSystem)
        {
            this._levelsManager = levelsManager;
            this._loadingScreen = loadingScreen;
            this._coinManager = coinManager;
            this._saveLoadSystem = saveLoadSystem;
        }

        public async void Enter()
        {
            _loadingScreen.Show();

            var currentLevelIndex = await _levelsManager.LoadLevelAsync();

            await _coinManager.Initialize(_saveLoadSystem.GameData.CoinData,  currentLevelIndex);

            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}