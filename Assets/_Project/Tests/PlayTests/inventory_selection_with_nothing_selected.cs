using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace ui_tests
{
    public class inventory_selection_with_nothing_selected
    {
        [Test]
        public void clicking_on_empty_slot_selects_slot()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var slots = inventoryPanel.Slots.ToArray();
            var slot = slots[0];
            
            slot.OnPointerDown(null);
            
            Assert.AreSame( slot, inventoryPanel.Selected);

        }
        
        [Test]
        public void clicking_empty_slot_does_not_select_slot()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(0);
            var slots = inventoryPanel.Slots.ToArray();
            var slot = slots[0];
            
            slot.OnPointerDown(null);
            
            Assert.IsNull(inventoryPanel.Selected);

        }
    }

    public class inventory_selection_with_non_empty_slot_selected
    {
        [Test]
        public void clicking_slot_moves_selected_item_to_clicked_slot()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(2);

            var slots = inventoryPanel.Slots.ToArray();
           
            var slot0 = slots[0];
            var slot1 = slots[1];
            var item0 = slot0.Item;
            var item1 = slot1.Item;
            
            Assert.IsNotNull( item0 );
            Assert.IsNotNull( item1 );

            slot0.OnPointerDown(null);
            slot1.OnPointerDown( null );
            
            Assert.AreSame( item0, slot1.Item);
            Assert.AreSame( item1, slot0.Item);

        }

        [Test]
        public void clicking_slot_clears_selection()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(2);

            var slots = inventoryPanel.Slots.ToArray();

            var slot0 = slots[0];
            var slot1 = slots[1];

            slot0.OnPointerDown(null);
            Assert.IsNotNull(inventoryPanel.Selected);

            slot1.OnPointerDown(null);
            Assert.IsNull(inventoryPanel.Selected);
        }
    }
}