using System;
using System.Collections.Generic;
using UnityEngine;

namespace JasonRPG
{
    public class StateMachine
    {
        private List<StateTransition> _stateTransitions = new List<StateTransition>();
        private List<StateTransition> _anyStateTransitions = new List<StateTransition>();

        public event Action<IState> OnStateChanged;
        public IState CurrentState { get; private set; }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            var stateTransition = new StateTransition(from, to, predicate);
            _stateTransitions.Add(stateTransition);
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
            OnStateChanged?.Invoke(CurrentState);
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
                if (transition.Predicate())
                {
                    return transition;
                }
            }

            foreach (var transition in _stateTransitions)
            {
                if (transition.FromState == CurrentState && transition.Predicate())
                {
                    return transition;
                }
            }

            return null;
        }
    }
}