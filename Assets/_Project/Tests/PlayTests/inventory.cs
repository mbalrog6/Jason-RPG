﻿using JasonRPG.Inventory;
using NUnit.Framework;
using UnityEngine;

namespace edit_mode_ui_tests
{
    public class inventory
    {
        // Add Items
        [Test]
        public void can_add_items()
        {
            Inventory inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item = new GameObject("Item", typeof(SphereCollider)).AddComponent<Item>();
            inventory.Pickup(item);
            
            Assert.AreEqual(1, inventory.Count);
        }

        // Place into specific slot
        [Test]
        public void place_an_item_into_specific_slot()
        {
            Inventory inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item = new GameObject("Item", typeof(SphereCollider)).AddComponent<Item>();
            
            inventory.Pickup(item, 5);
            
            Assert.AreEqual(item, inventory.GetItemInSlot(5));
        }

        // Change Slots / Move
        [Test]
        public void can_move_item_to_empty_slot()
        {
            Inventory inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item = new GameObject("Item", typeof(SphereCollider)).AddComponent<Item>();
            
            inventory.Pickup(item);
            
            Assert.AreEqual(item, inventory.GetItemInSlot(0));

            inventory.Move(0, 4);
            
            Assert.AreEqual( item, inventory.GetItemInSlot(4));
        }
        // Remove Items
        // Drop Itmes on Ground
        // Hotkey/Hotbar assignment
        // Change Visuals
        // Modify Stats
        // Persist & Load
    }
}