using System;
using UnityEngine;

namespace JasonRPG
{
    public class PlayerInput : IPlayerInput
    {
        public event Action<MovementMode> MovementSwitched;
        public event Action<int> HotkeyPressed;
        public float Vertical => Input.GetAxis("Vertical");
        public float Horizontal => Input.GetAxis("Horizontal");
        public float MouseX => Input.GetAxis("Mouse X");


        public void Tick()
        {
            if (HotkeyPressed == null)
                return;
            
            for (int i = 0; i < 9; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    HotkeyPressed(i);
                }
            }

            TriggerEventForMovementSwitched();
        }

        private void TriggerEventForMovementSwitched()
        {
            if (MovementSwitched == null)
                return;

            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                MovementSwitched(MovementMode.WASD);
            }

            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                MovementSwitched(MovementMode.Navmesh);
            }
        }
    }
}