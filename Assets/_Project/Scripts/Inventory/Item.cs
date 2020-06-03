using System;
using System.Collections;
using JasonRPG.UI;
using UnityEngine;

namespace JasonRPG.Inventory
{
    [RequireComponent(typeof(Collider))]
    public class Item : MonoBehaviour
    {
        [SerializeField] private UseAction[] actions = new UseAction[0];
        [SerializeField] private CrosshairDefinition crosshairDefinition;
        public UseAction[] Actions => actions;
        public CrosshairDefinition CrosshairDefinition => crosshairDefinition;

        private bool _wasPickedUp;

        
        private void OnTriggerEnter(Collider other)
        {
            if (_wasPickedUp)
                return;

            var inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.Pickup(this);
                _wasPickedUp = true;
            }
        }

        private void OnValidate()
        {
            var collider = GetComponent<Collider>();
            collider.isTrigger = true;
        }
    }
}