using JasonRPG.Inventory;
using UnityEngine;

namespace JasonRPG.Entity
{
    [RequireComponent(typeof(Inventory.Inventory))]
    public class NpcLoot : MonoBehaviour
    {
        [SerializeField] private Item[] itemsPrefabs;
        
        private Inventory.Inventory _inventory;
        private EntityStateMachine _entityStateMachine;

        private void Start()
        {
            _inventory = GetComponent<Inventory.Inventory>();
            _entityStateMachine = GetComponent<EntityStateMachine>();
            _entityStateMachine.OnEntityStateChanged += Handle_OnEntityStateChanged;

            foreach (var itemPrefab in itemsPrefabs)
            {
                var itemInstance = GameObject.Instantiate(itemPrefab);
                _inventory.Pickup(itemInstance);
            }
        }

        private void Handle_OnEntityStateChanged(IState state)
        {
            Debug.Log( $"HandleEntityStateChanged {state.GetType()}");
            if (state is Dead)
            {
                DropLoot();
            }
        }

        private void DropLoot()
        {
            foreach (var item in _inventory.Items)
            {
                if (item != null)
                {
                    LootSystem.Drop(item, transform);
                }
                
            }
            _inventory.Items.Clear();
        }
    }
}