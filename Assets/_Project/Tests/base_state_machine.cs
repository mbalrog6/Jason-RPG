using System.Collections;
using JasonRPG;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class base_state_machine
    {
        [Test]
        public void initial_set_state_switches_to_state()
        {
            var stateMachine = new StateMachine();
            IState firstState = Substitute.For<IState>();
            stateMachine.Add(firstState);
            stateMachine.SetState(firstState);
            
            Assert.AreSame(firstState, stateMachine.CurrentState);
        }
        
        [Test]
        public void subsequent_set_state_switches_to_state()
        {
            var stateMachine = new StateMachine();
            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();
            stateMachine.Add(firstState);
            stateMachine.Add(secondState);
            
            stateMachine.SetState(firstState);
            Assert.AreNotSame(stateMachine.CurrentState, secondState);
            
            stateMachine.SetState(secondState);
            Assert.AreSame(secondState, stateMachine.CurrentState);
        }
        
        [Test]
        public void transition_switches_state_when_condition_is_met()
        {
            var stateMachine = new StateMachine();
            
            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();
            stateMachine.Add(firstState);
            stateMachine.Add(secondState);

            bool ShouldTransitionToNewState() => true;
            stateMachine.AddTransition(firstState, secondState, () => ShouldTransitionToNewState());
            
            stateMachine.SetState(firstState);
            Assert.AreSame(firstState, stateMachine.CurrentState);
            stateMachine.Tick();
            
            Assert.AreSame(secondState, stateMachine.CurrentState);
        }
        
        [Test]
        public void transition_does_not_switch_state_when_condition_is_not_met()
        {
            var stateMachine = new StateMachine();
            
            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();
            stateMachine.Add(firstState);
            stateMachine.Add(secondState);

            bool ShouldNotTransitionToNewState() => false;
            stateMachine.AddTransition(firstState, secondState, () => ShouldNotTransitionToNewState());
            
            stateMachine.SetState(firstState);
            Assert.AreSame(firstState, stateMachine.CurrentState);
            stateMachine.Tick();
            
            Assert.AreSame(firstState, stateMachine.CurrentState);
        }
        
        [Test]
        public void transition_from_any_state_to_new_state_when_condition_is_met()
        {
            var stateMachine = new StateMachine();
            
            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();
            stateMachine.Add(firstState);
            stateMachine.Add(secondState);

            bool ShouldTransitionToNewState() => true;
            stateMachine.AddAnyTransition(secondState, () => ShouldTransitionToNewState());
            
            stateMachine.SetState(firstState);
            Assert.AreSame(firstState, stateMachine.CurrentState);
            stateMachine.Tick();
            
            Assert.AreSame(secondState, stateMachine.CurrentState);
        }
        
    }
}
