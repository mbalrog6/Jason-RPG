using System;
using System.Collections.Generic;
using JasonRPG.Inventory;
using NSubstitute.Core;
using UnityEngine;

namespace JasonRPG.UI
{

    public class Hotbar : MonoBehaviour
    {
        private Inventory.Inventory _inventory;
        private Slot[] _slots;
        private Player _player;

        private void OnEnable()
        {
            _player = FindObjectOfType<Player>();
            _player.ControllerInput.HotkeyPressed += Handle_HotkeyPressed;
            _inventory = FindObjectOfType<Inventory.Inventory>();
            _inventory.ItemPickedUp += Handle_ItemPickedUp;
            _slots = GetComponentsInChildren<Slot>();
        }

        private void OnDisable()
        {
            _player.ControllerInput.HotkeyPressed -= Handle_HotkeyPressed;
            _inventory.ItemPickedUp -= Handle_ItemPickedUp;
        }

        private void Handle_HotkeyPressed(int hotbarKeyPressed)
        {
            if (hotbarKeyPressed >= _slots.Length ||
                hotbarKeyPressed < 0)
            {
                return;
            }

            if (_slots[hotbarKeyPressed].IsEmplty == false)
            {
                _inventory.Equip(_slots[hotbarKeyPressed].Item);
            }
        }

        private void Handle_ItemPickedUp(Item item)
        {
            Slot slot = FindNextOpenSlot();
            if (slot != null)
            {
                slot.SetItem(item);
            }
        }

        private Slot FindNextOpenSlot()
        {
            foreach (Slot slot in _slots)
            {
                if (slot.IsEmplty)
                {
                    return slot;
                }
            }

            return null;
        }
    }
}


