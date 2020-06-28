using System;
using UnityEngine;

namespace JasonRPG
{
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        public static IPlayerInput Instance { get; set; }
        public event Action<MovementMode> MovementSwitched;
        public event Action<int> HotkeyPressed;
        public float Vertical => Input.GetAxis("Vertical");
        public float Horizontal => Input.GetAxis("Horizontal");
        public float MouseX => Input.GetAxis("Mouse X");
        public Vector2 MousePosition => Input.mousePosition;
        public bool PausePressed { get; }

        private void Awake() => Instance = this;

        private void Update() => Tick();


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

        public bool GetKeyDown(KeyCode keyCode) => Input.GetKeyDown(keyCode);

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