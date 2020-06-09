using System;
using System.Collections.Generic;
using UnityEngine;

namespace JasonRPG
{
    public class StateMachine
    {
        private List<IState> _states = new List<IState>();
        private Dictionary<IState, List<StateTransition>> _stateTransitions = new Dictionary<IState, List<StateTransition>>();
        private List<StateTransition> _anyStateTransitions = new List<StateTransition>();
        public IState CurrentState { get; private set; }

        public void Add(IState state)
        {
            _states.Add(state);
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (_stateTransitions.ContainsKey(from) == false)
            {
                _stateTransitions[from] = new List<StateTransition>();
            }

            var stateTransition = new StateTransition(from, to, predicate);
            _stateTransitions[from].Add(stateTransition);
        }

        public void AddAnyTransition(IState to, Func<bool> predicate)
        {
            var stateTranisions = new StateTransition(null, to, predicate);
            _anyStateTransitions.Add(stateTranisions);
        }

        public void SetState(IState state)
        {
            if (CurrentState == state)
                return;

            CurrentState?.OnExit();
            CurrentState = state;
            Debug.Log($"Changed to state: {state}");
            CurrentState.OnEnter();
        }

        public void Tick()
        {
            StateTransition transition = CheckForTransition();
            if (transition != null)
            {
                SetState(transition.ToNewState);
            }

            CurrentState.Tick();
        }

        private StateTransition CheckForTransition()
        {
            foreach (var transition in _anyStateTransitions)
            {
                if( transition != null )
                {
                    if (transition.Predicate())
                    {
                        return transition;
                    }
                }
            }
            
            if (_stateTransitions.ContainsKey(CurrentState))
            {
                foreach (var transition in _stateTransitions[CurrentState])
                {
                    if (transition.Predicate())
                    {
                        return transition;
                    }
                }
            }

            return null;
        }
    }
}