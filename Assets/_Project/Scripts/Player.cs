using System;
using UnityEngine;

namespace JasonRPG
{
    public class Player : MonoBehaviour
    {
        private CharacterController _characterController;
        private IMover _mover;
        private Rotator _rotator;
        public IPlayerInput ControllerInput { get; set; } = new PlayerInput();

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _mover = new Mover(this );
            _rotator = new Rotator(this);
        }

        private void OnEnable()
        {
            ControllerInput.MovementSwitched += Handle_MovementSwitched;
        }

        private void OnDisable()
        {
            ControllerInput.MovementSwitched -= Handle_MovementSwitched;
        }

        private void Update()
        {
            _mover.Tick();
            _rotator.Tick();
            ControllerInput.Tick();
        }

        private void Handle_MovementSwitched( MovementMode movementMode )
        {
            if (movementMode == MovementMode.WASD)
            {
                _mover = new Mover(this);
            }
            else if (movementMode == MovementMode.Navmesh)
            {
                _mover = new NavMeshMover(this);
            }
        }
    }
}