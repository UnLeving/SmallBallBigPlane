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
            LoadLevelState loadLevelState,
            RestartState restartState)
        {
            bootstrapState.SetStateMachine(this);
            _states.Add(bootstrapState.GetType(), bootstrapState);
            
            _states.Add(mainMenuState.GetType(), mainMenuState);
            
            gameLoopState.SetStateMachine(this);
            _states.Add(gameLoopState.GetType(), gameLoopState);
            
            _states.Add(exitState.GetType(), exitState);
            
            _states.Add(winLevelState.GetType(), winLevelState);
            
            looseLevelState.SetStateMachine(this);
            _states.Add(looseLevelState.GetType(), looseLevelState);
            
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
            if(_currentState is IStateExitable exitable)
            {
                exitable?.Exit();
            }

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