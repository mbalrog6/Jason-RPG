using System.Collections;
using JasonRPG;
using NSubstitute;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace a_player
{
    public static class Helpers
    {
        public static IEnumerator LoadMenuScene()
        {
            var operation = SceneManager.LoadSceneAsync("Menu");
            while (operation.isDone == false)
                yield return null;
        }

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

        public static IEnumerator LoadEntityStateMachineTestScene()
        {
            var operation = SceneManager.LoadSceneAsync("EnityStateMachineTests");
            while (operation.isDone == false)
                yield return null;
            
            operation = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;
        }

        public static Player GetPlayer()
        {
            var player = GameObject.FindObjectOfType<Player>();
            return player;
        }

        public static float CalculateTurn(Quaternion originalRotation, Quaternion transformRotation)
        {
            var cross = Vector3.Cross(originalRotation * Vector3.forward, transformRotation * Vector3.forward);
            var dot = Vector3.Dot(cross, Vector3.up);
            return dot;
        }
    }
}