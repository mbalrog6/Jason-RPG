﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class a_moving_cube
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator moving_forward_changes_position()
        {
            // ARRANGE
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = Vector3.zero;

            for (int i = 0; i < 10; i++)
            {
                // ACT
                cube.transform.position += Vector3.forward;
                yield return null;
                
                // ASSERT
                Assert.AreEqual(i+1, cube.transform.position.z );
            }
        }

        [UnityTest]
        public IEnumerator growing_a_cube()
        {
            // ARRANGE
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = Vector3.zero;
            cube.transform.localScale = Vector3.one;

            for (int i = 1; i < 10; i++)
            {
                // ACT
                cube.transform.localScale = Vector3.one * i;
                yield return new WaitForSeconds(0.5f);
                
                // ASSERT
                Assert.AreEqual(i, cube.transform.localScale.x);
                Assert.AreEqual(i, cube.transform.localScale.y);
                Assert.AreEqual(i, cube.transform.localScale.z);
            }
            
        }
    }
}
