using System.Collections;
using JasonRPG;
using JasonRPG.Inventory;
using JasonRPG.UI;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace a_player
{
    public class moving_into_an_item
    {
        private Player player;
        private Item item;
        
        [UnitySetUp]
        public IEnumerator init()
        {
            yield return Helpers.LoadItmesTestScene();
            player = Helpers.GetPlayer();
            item = Object.FindObjectOfType<Item>();
        }
        
        [UnityTest]
        public IEnumerator picks_up_and_equips_item()
        {
            player.ControllerInput.Vertical.Returns(1f);

            Assert.AreNotSame(item, player.GetComponent<Inventory>().ActiveItem);
            
            yield return new WaitForSeconds(3f);

            Assert.AreSame(item, player.GetComponent<Inventory>().ActiveItem);
        }
        
        [UnityTest]
        public IEnumerator changes_crosshair_to_item_crosshair()
        {
            var crosshair = Object.FindObjectOfType<Crosshair>();
            
            item.transform.position = player.transform.position;
            
            Assert.AreNotSame(item.CrosshairDefinition.Sprite, crosshair.GetComponent<Image>().sprite);
            
            yield return new WaitForFixedUpdate();

            Assert.AreEqual(item.CrosshairDefinition.Sprite, crosshair.GetComponent<Image>().sprite);
        }
        
        [UnityTest]
        public IEnumerator changes_slot1_icon_to_match_item_icon()
        {
            var hotbar = Object.FindObjectOfType<Hotbar>();
            var slotOne = hotbar.GetComponentInChildren<Slot>();
            
            item.transform.position = player.transform.position;
            
            Assert.AreNotSame(item.Icon, slotOne.Icon);
            
            yield return new WaitForFixedUpdate();

            Assert.AreEqual(item.Icon, slotOne.Icon);
        }
    }
}