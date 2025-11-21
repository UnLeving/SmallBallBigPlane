using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SmallBallBigPlane.Infrastructure.FSM.States;
using SmallBallBigPlane.UI;
using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.FSM
{
    public class StateMachine : IInitializable
    {
        public IState CurrentState => _currentState;

        private readonly Dictionary<Type, IState> _states = new();

        private IState _currentState;

        public StateMachine(BootstrapState bootstrapState,
            MainMenuState mainMenuState,
            GameLoopState gameLoopState,
            ExitState exitState,
            WinLevelState winLevelState,
            LooseLevelState looseLevelState, 
            MainMenu mainMenu,
            LoadLevelState loadLevelState,
            RestartState restartState)
        {
            bootstrapState.SetStateMachine(this);
            _states.Add(bootstrapState.GetType(), bootstrapState);
            
            mainMenuState.SetStateMachine(this);
            _states.Add(mainMenuState.GetType(), mainMenuState);
            
            gameLoopState.SetStateMachine(this);
            _states.Add(gameLoopState.GetType(), gameLoopState);
            
            exitState.SetStateMachine(this);
            _states.Add(exitState.GetType(), exitState);
            
            winLevelState.SetStateMachine(this);
            _states.Add(winLevelState.GetType(), winLevelState);
            
            looseLevelState.SetStateMachine(this);
            _states.Add(looseLevelState.GetType(), looseLevelState);
            
            mainMenu.SetStateMachine(this);
            
            loadLevelState.SetStateMachine(this);
            _states.Add(loadLevelState.GetType(), loadLevelState);
            
            restartState.SetStateMachine(this);
            _states.Add(restartState.GetType(), restartState);
        }

        public UniTask Initialize()
        {
            Enter<BootstrapState>();

            return UniTask.CompletedTask;
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state?.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            //Debug.Log("StateMachine.ChangeState: " + typeof(TState));
            _currentState?.Exit();

            var state = GetState<TState>();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            if (_states.TryGetValue(typeof(TState), out IState newState) == false)
            {
                Debug.LogError($"{typeof(TState)} doesn't exist.");
            }

            return (TState)newState;
        }
    }
}