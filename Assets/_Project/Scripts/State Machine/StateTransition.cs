using System;

namespace JasonRPG
{
    public class StateTransition
    {
        public readonly IState FromState;
        public readonly IState ToNewState;
        public readonly Func<bool> Predicate;

        public StateTransition(IState fromState, IState toNewState, Func<bool> predicate)
        {
            FromState = fromState;
            ToNewState = toNewState;
            Predicate = predicate;
        }
    }
}