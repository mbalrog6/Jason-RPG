using System.Collections;
using JasonRPG;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace a_player
{
    public static class Helpers
    {
        public static IEnumerator LoadMovementTestsScene()
        {
            var operation = SceneManager.LoadSceneAsync("Movement Tests");
            while (operation.isDone == false)
                yield return null;
        }

        public static IEnumerator LoadItmesTestScene()
        {
            var operation = SceneManager.LoadSceneAsync("Item Tests");
            while (operation.isDone == false)
                yield return null;
            
            operation = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;
        }

        public static Player GetPlayer()
        {
            var player = GameObject.FindObjectOfType<Player>();
         
            var testPlayerInput = Substitute.For<IPlayerInput>();
            player.ControllerInput = testPlayerInput;
            return player;
        }

        public static float CalculateTurn(Quaternion originalRotation, Quaternion transformRotation)
        {
            var cross = Vector3.Cross(originalRotation * Vector3.forward, transformRotation * Vector3.forward);
            var dot = Vector3.Dot(cross, Vector3.up);
            return dot;
        }
        
        
    }

    public class with_positive_vertical_input
    {
        [UnityTest]
        public IEnumerator moves_forward()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();

            player.ControllerInput.Vertical.Returns(1f);

            float startingZPosition = player.transform.position.z;

            yield return new WaitForSeconds(1f);

            float endingZPosition = player.transform.position.z;

            Assert.Greater(endingZPosition, startingZPosition);
        }
    }

    public class with_negative_vertical_input
    {
        [UnityTest]
        public IEnumerator moves_backwards()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();

            player.ControllerInput.Vertical.Returns(-1f);

            float startingZPosition = player.transform.position.z;

            yield return new WaitForSeconds(1f);

            float endingZPosition = player.transform.position.z;

            Assert.Less(endingZPosition, startingZPosition);
        }
    }

    public class with_positive_horizontal_input
    {
        [UnityTest]
        public IEnumerator moves_right()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();
            player.ControllerInput.Horizontal.Returns(1f);

            float startingXPosition = player.transform.position.x;

            yield return new WaitForSeconds(1f);

            float endingXPosition = player.transform.position.x;

            Assert.Greater(endingXPosition, startingXPosition);
        }
    }
    
    public class with_negative_horizontal_input
    {
        [UnityTest]
        public IEnumerator moves_left()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();
            player.ControllerInput.Horizontal.Returns(-1f);

            float startingXPosition = player.transform.position.x;

            yield return new WaitForSeconds(1f);

            float endingXPosition = player.transform.position.x;

            Assert.Less(endingXPosition, startingXPosition);
        }
    }

    public class with_negative_mouse_x
    {
        [UnityTest]
        public IEnumerator turns_left()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();

            player.ControllerInput.MouseX.Returns(-1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(2f);

            float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Less( turnAmount, 0);
        }
        
        [UnityTest]
        public IEnumerator turns_right()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();

            player.ControllerInput.MouseX.Returns(1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(2f);

            float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Greater( turnAmount, 0);
        }
    }
}