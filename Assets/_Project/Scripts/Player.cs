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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _mover = new Mover(this);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _mover = new NavMeshMover(this );
            }
        
            _mover.Tick();
            _rotator.Tick();
        }
    }
}