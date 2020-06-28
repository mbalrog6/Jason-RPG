using JasonRPG.Inventory;
using UnityEditor;
using UnityEngine;

namespace ui_tests
{
    public static class inventory_helpers
    {
        public static UIInventoryPanel GetInventoryPanelWithItems(int numberOfItems)
        {
            var prefab =
                AssetDatabase.LoadAssetAtPath<UIInventoryPanel>("Assets/_project/Prefabs/UI/InventoryPanel.prefab");
            var panel = Object.Instantiate(prefab);

            var inventory = GetInventory(numberOfItems);
            panel.Bind(inventory);

            return panel;
        }

        public static Inventory GetInventory(int numberOfItems = 0)
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            for (int i = 0; i < numberOfItems; i++)
            {
                var item = GetItem(i);
                inventory.Pickup(item);
            }

            return inventory;
        }

        public static Item GetItem(int? index = null)
        {
            var prefab =
                AssetDatabase.LoadAssetAtPath<Item>("Assets/_project/Prefabs/Items/TestItem.prefab");
            return Object.Instantiate(prefab);
        }

        public static UISelectionCursor GetSelectionCursor()
        {
            var prefab =
                AssetDatabase.LoadAssetAtPath<UISelectionCursor>("Assets/_project/Prefabs/UI/SelectionCursor.prefab");
           return Object.Instantiate(prefab);
        }
    }

    
}