using System.Collections;
using a_player;
using JasonRPG;
using JasonRPG.Entity;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace state_machine
{
    public class entity_state_machine
    {
        [SetUp]
        public void setup()
        {
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
        }
        
        [UnityTest]
        public IEnumerator start_in_idle_state()
        {
            yield return Helpers.LoadEntityStateMachineTestScene();
            var stateMachine = GameObject.FindObjectOfType<EntityStateMachine>();
            Assert.AreEqual(typeof(Idle), stateMachine.CurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator switches_to_chasePlayer_state_when_in_chase_range()
        {
            yield return Helpers.LoadEntityStateMachineTestScene();
            var player = Helpers.GetPlayer();
            var stateMachine = GameObject.FindObjectOfType<EntityStateMachine>();
            
            yield return null;
            Assert.AreEqual(typeof(Idle), stateMachine.CurrentStateType);
            player.transform.position = stateMachine.transform.position + new Vector3(5f, 0, 0);
            yield return null;
            Assert.AreEqual(typeof(ChasePlayer), stateMachine.CurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator switches_to_dead_state_only_once_health_reaches_zero()
        {
            yield return Helpers.LoadEntityStateMachineTestScene();
            var stateMachine = GameObject.FindObjectOfType<EntityStateMachine>();
            var entity = stateMachine.GetComponent<Entity>();
            
            yield return null;
            Assert.AreEqual(typeof(Idle), stateMachine.CurrentStateType);
            
            entity.TakeHit(entity.Health - 1);
            yield return null;
            Assert.AreEqual(typeof(Idle), stateMachine.CurrentStateType);
 
            entity.TakeHit(entity.Health);
            yield return null;
            Assert.AreEqual(typeof(Dead), stateMachine.CurrentStateType);
        }
    }
}