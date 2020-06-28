using System;
using System.Collections;
using UnityEngine;

namespace JasonRPG.Inventory
{
    [RequireComponent(typeof(Collider))]
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] private UseAction[] actions = new UseAction[0];
        [SerializeField] private CrosshairDefinition crosshairDefinition;
        [SerializeField] private Sprite icon;
        [SerializeField] private StatMod[] statMods;
        public StatMod[] StatMods => statMods;
        
        public event Action OnPickedUp;
        public UseAction[] Actions => actions;
        public CrosshairDefinition CrosshairDefinition => crosshairDefinition;
        public Sprite Icon => icon;
        public bool WasPickedUp { get; set; }


        private void OnTriggerEnter(Collider other)
        {
            if (WasPickedUp)
                return;

            var inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.Pickup(this);
                OnPickedUp?.Invoke();
            }
        }

        private void OnValidate()
        {
            var collider = GetComponent<Collider>();
            collider.isTrigger = true;
        }
    }
}