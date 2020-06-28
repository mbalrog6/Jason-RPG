using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using JasonRPG.Inventory;
using UnityEditor.Graphs;
using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
{
    public event Action OnSelectionChanged;
    public IEnumerable<UIInventorySlot> Slots => _slots;
    private UIInventorySlot[] _slots;
    public int SlotCount => _slots.Length;
    public UIInventorySlot Selected { get; private set; }

    private Inventory _inventory;


    private void Awake()
    {
        _slots = FindObjectsOfType<UIInventorySlot>()
            .OrderByDescending(t => t.SortIndex)
            .ThenBy(t => t.name)
            .ToArray();
        RegisterSlotsForClickCallback();
    }

    private void RegisterSlotsForClickCallback()
    {
        foreach (var slot in Slots)
        {
            slot.OnSlotClicked += Handle_SlotClicked;
        }
    }

    public void Bind(Inventory inventory)
    {
        if (_inventory != null)
        {
            _inventory.ItemPickedUp -= Handle_ItemPickedUp;
            _inventory.OnItemChanged -= Handle_OnItemChanged;
        }

        _inventory = inventory;

        if (_inventory != null)
        {
            _inventory.ItemPickedUp += Handle_ItemPickedUp;
            _inventory.OnItemChanged += Handle_OnItemChanged;
            RefreshSlots();
        }
        else
        {
            ClearSlots();
        }
    }

    private void Handle_OnItemChanged(int slotNumber)
    {
        _slots[slotNumber].SetItem(_inventory.GetItemInSlot(slotNumber));
    }

    private void Handle_SlotClicked(UIInventorySlot slot)
    {
        if (Selected != null)
        {
            Swap(slot);
            Selected.BecomesUnSelected();
            Selected = null;
            
        }
        else if(slot.IsEmpty == false)
        {
            Selected = slot;
            Selected.BecomesSelected();
        }
        OnSelectionChanged?.Invoke();
        
    }

    private void Swap(UIInventorySlot slot)
    {
        _inventory.Move(GetSlotIndex(Selected), GetSlotIndex(slot));
    }

    private int GetSlotIndex(UIInventorySlot selected)
    {
        for (int i = 0; i < SlotCount; i++)
        {
            if (_slots[i] == selected)
            {
                return i;
            }

        }

        return -1;
    }

    private void Handle_ItemPickedUp(Item item)
    {
        RefreshSlots();
    }

    private void ClearSlots()
    {
        for (var i = 0; i < _slots.Length; i++)
        {
            var slot = _slots[i];
            slot.Clear();
        }
    }

    private void RefreshSlots()
    {
        for (var i = 0; i < _slots.Length; i++)
        {
            var slot = _slots[i];

            if (_inventory.Items.Count > i)
            {
                slot.SetItem(_inventory.Items[i]);
            }
            else
            {
                slot.Clear();
            }
        }
    }
}