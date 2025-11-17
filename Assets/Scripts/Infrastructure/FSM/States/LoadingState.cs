using HelpersAndExtensions.SaveSystem;
using SmallBallBigPlane.Collectables;
using SmallBallBigPlane.Infrastructure.Services;
using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.FSM.States
{
    public class LoadingState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly CoinManager _coinManager;
        private readonly SaveLoadSystem _saveLoadSystem;

        private StateMachine _stateMachine;
        private AsyncOperation asyncOp;

        public LoadingState(SceneLoader sceneLoader, CoinManager coinManager, SaveLoadSystem saveLoadSystem)
        {
            this._sceneLoader = sceneLoader;
            this._coinManager = coinManager;
            this._saveLoadSystem = saveLoadSystem;
        }

        public void Enter()
        {
            asyncOp = _sceneLoader.LoadSceneAsync(SceneLoader.Scene.LoadingScene);

            asyncOp.completed += AsyncOpOnCompleted;
        }

        private void AsyncOpOnCompleted(AsyncOperation obj)
        {
            _coinManager.Initialize(_saveLoadSystem.GameData.CoinData);
            
            _stateMachine.Enter<GameLoopState>();
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Exit()
        {
            asyncOp.completed -= AsyncOpOnCompleted;
        }
    }
}