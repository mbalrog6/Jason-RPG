using System.Collections;
using a_player;
using JasonRPG;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace state_machine
{
    public class game_state_machine : player_input_test
    {
        [TearDown]
        public void teardow()
        {
            GameObject.Destroy(Object.FindObjectOfType<GameStateMachine>());
        }
        
        [UnityTest]
        public IEnumerator switches_to_loading_when_level_to_load_selected()
        {
            yield return Helpers.LoadMenuScene();
            var statemachine = GameObject.FindObjectOfType<GameStateMachine>();
            
            Assert.AreEqual(typeof(Menu), statemachine.GetCurrentStateType);
            PlayButton.LevelToLoad = "Level 1";
            yield return null;

            Assert.AreEqual(typeof(Loadlevel), statemachine.GetCurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator switches_to_play_when_level_to_load_completed()
        {
           yield return Helpers.LoadMenuScene();
            var statemachine = GameObject.FindObjectOfType<GameStateMachine>();
            
            Assert.AreEqual(typeof(Menu), statemachine.GetCurrentStateType);
            PlayButton.LevelToLoad = "Level 1";
            yield return null;

            Assert.AreEqual(typeof(Loadlevel), statemachine.GetCurrentStateType);
            
            yield return new WaitUntil(() => statemachine.GetCurrentStateType == typeof(Play));
            Assert.AreEqual(typeof(Play), statemachine.GetCurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator switches_from_play_to_pause_when_pause_button_pressed()
        {
            yield return Helpers.LoadMenuScene();
            var statemachine = GameObject.FindObjectOfType<GameStateMachine>();
            
            Assert.AreEqual(typeof(Menu), statemachine.GetCurrentStateType);
            PlayButton.LevelToLoad = "Level 1";
            
            yield return new WaitUntil(() => statemachine.GetCurrentStateType == typeof(Play));

            PlayerInput.Instance.PausePressed.Returns(true);
            yield return null;
            
            Assert.AreEqual(typeof(Paused), statemachine.GetCurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator only_allows_one_instance_to_exist()
        {
            var firstGameStateMachine = new GameObject("First State Machine").AddComponent<GameStateMachine>();
            var secondGameStateMachine = new GameObject("Second State Machine").AddComponent<GameStateMachine>();

            yield return null;
            Assert.IsTrue(firstGameStateMachine != null);
            Assert.IsTrue(secondGameStateMachine == null);
        }
    }
}