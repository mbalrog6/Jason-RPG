using UnityEngine;

namespace JasonRPG
{
    [RequireComponent( typeof(Inventory.Inventory))]
    public class Player : MonoBehaviour
    {
        private CharacterController _characterController;
        private IMover _mover;
        private Rotator _rotator;
        private Inventory.Inventory _inventory;

        public Stats Stats { get; private set; }
       
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _mover = new Mover(this );
            _rotator = new Rotator(this);
            _inventory = GetComponent<Inventory.Inventory>();
            
            PlayerInput.Instance.MovementSwitched += Handle_MovementSwitched;

            Stats = new Stats();
            Stats.Bind(_inventory);
        }
        
        private void Update()
        {
            if (Paused.Active)
                return;
            
            _mover.Tick();
            _rotator.Tick();
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