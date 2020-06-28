using System.Collections;
using JasonRPG;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_player
{
    public class player_input_test
    {
        [SetUp]
        public void setup()
        {
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
        }
    }
    public class with_positive_vertical_input : player_input_test

    {
        [UnityTest]
        public IEnumerator moves_forward()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();

            PlayerInput.Instance.Vertical.Returns(1f);

            float startingZPosition = player.transform.position.z;

            yield return new WaitForSeconds(2f);

            float endingZPosition = player.transform.position.z;

            Assert.Greater(endingZPosition, startingZPosition);
        }
    }

    public class with_negative_vertical_input : player_input_test
    {
        [UnityTest]
        public IEnumerator moves_backwards()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();

            PlayerInput.Instance.Vertical.Returns(-1f);

            float startingZPosition = player.transform.position.z;

            yield return new WaitForSeconds(2f);

            float endingZPosition = player.transform.position.z;

            Assert.Less(endingZPosition, startingZPosition);
        }
    }

    public class with_positive_horizontal_input : player_input_test
    {
        [UnityTest]
        public IEnumerator moves_right()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();
            PlayerInput.Instance.Horizontal.Returns(1f);

            float startingXPosition = player.transform.position.x;

            yield return new WaitForSeconds(2f);

            float endingXPosition = player.transform.position.x;

            Assert.Greater(endingXPosition, startingXPosition);
        }
    }
    
    public class with_negative_horizontal_input : player_input_test
    {
        [UnityTest]
        public IEnumerator moves_left()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();
            PlayerInput.Instance.Horizontal.Returns(-1f);

            float startingXPosition = player.transform.position.x;

            yield return new WaitForSeconds(2f);

            float endingXPosition = player.transform.position.x;

            Assert.Less(endingXPosition, startingXPosition);
        }
    }

    public class with_negative_mouse_x : player_input_test
    {
        [UnityTest]
        public IEnumerator turns_left()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();

            PlayerInput.Instance.MouseX.Returns(-1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Less( turnAmount, 0);
        }
        
        [UnityTest]
        public IEnumerator turns_right()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();

            PlayerInput.Instance.MouseX.Returns(1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Greater( turnAmount, 0);
        }
    }
}