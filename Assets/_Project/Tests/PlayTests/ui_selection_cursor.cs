using System;
using System.Collections;
using System.Linq;
using a_player;
using JasonRPG;
using NSubstitute;
using NUnit.Framework;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ui_tests
{
    public class ui_selection_cursor
    {
        [Test]
        public void in_default_state_shows_no_icon()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var uiCursor = inventory_helpers.GetSelectionCursor();
            Assert.IsFalse(uiCursor.IconVisable);
            Assert.IsFalse(uiCursor.GetComponent<Image>().enabled);
        }

        [Test]
        public void with_item_selected_shows_item()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var uiCursor = inventory_helpers.GetSelectionCursor();
            inventoryPanel.Slots.First().OnPointerDown(null);
            Assert.IsTrue(uiCursor.IconVisable);
        }

        [Test]
        public void when_item_selected_sets_icon_image_to_correct_sprite()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var uiCursor = inventory_helpers.GetSelectionCursor();
            inventoryPanel.Slots.First().OnPointerDown(null);
            
            Assert.AreSame(inventoryPanel.Slots.First().Icon, uiCursor.Icon);
        }
        
        [Test]
        public void when_item_is_unselected_sets_icon_to_not_visible()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var uiCursor = inventory_helpers.GetSelectionCursor();
            Assert.IsFalse(uiCursor.IconVisable);
            
            inventoryPanel.Slots.First().OnPointerDown(null);
            Assert.IsTrue(uiCursor.IconVisable);
            
            inventoryPanel.Slots.First().OnPointerDown(null);
            Assert.IsFalse(uiCursor.IconVisable);
        }

        [UnityTest]
        public IEnumerator moves_with_mouse_cursor()
        {
            yield return Helpers.LoadItmesTestScene();
            var uiCursor = Object.FindObjectOfType<UISelectionCursor>();

            PlayerInput.Instance = Substitute.For<IPlayerInput>();

            for (int i = 0; i < 100; i++)
            {
                var mousePosition = new Vector2(100 + i, 100 + i);
                PlayerInput.Instance.MousePosition.Returns(mousePosition);

                yield return null;

                Assert.AreEqual(new Vector3(mousePosition.x, mousePosition.y, 0.0f), uiCursor.transform.position);
            }
        }

        [Test]
        public void disables_raycastTarget()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var uiCursor = inventory_helpers.GetSelectionCursor();
            var image = uiCursor.GetComponent<Image>();
            Assert.IsFalse(image.raycastTarget);
        }
    }
}