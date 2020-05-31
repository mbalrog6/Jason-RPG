using System.Collections;
using JasonRPG;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_player
{
    public class with_positive_vertical_input
    {
        [UnityTest]
        public IEnumerator moves_forward()
        {
            var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.localScale = new Vector3( 50f, 0.1f, 50f);
            floor.transform.position = Vector3.zero;

            var playerGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerGameObject.transform.position = new Vector3(0f, 1.1f, 0f);
            playerGameObject.AddComponent<CharacterController>();
            
            Player player = playerGameObject.AddComponent<Player>();
            var testPlayerInput = Substitute.For<IPlayerInput>();
            player.ControllerInput = testPlayerInput;
            testPlayerInput.Vertical.Returns(1f);

            float startingZPosition = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);

            float endingZPosition = player.transform.position.z;
            
            Assert.Greater(endingZPosition, startingZPosition);
        }
        
        [UnityTest]
        public IEnumerator moves_right()
        {
            var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.localScale = new Vector3( 50f, 0.1f, 50f);
            floor.transform.position = Vector3.zero;

            var playerGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerGameObject.transform.position = new Vector3(0f, 1.1f, 0f);
            playerGameObject.AddComponent<CharacterController>();
            
            Player player = playerGameObject.AddComponent<Player>();
            var testPlayerInput = Substitute.For<IPlayerInput>();
            player.ControllerInput = testPlayerInput;
            testPlayerInput.Horizontal.Returns(1f);

            float startingXPosition = player.transform.position.x;
            
            yield return new WaitForSeconds(5f);

            float endingXPosition = player.transform.position.x;
            
            Assert.Greater(endingXPosition, startingXPosition);
        }
    }
}