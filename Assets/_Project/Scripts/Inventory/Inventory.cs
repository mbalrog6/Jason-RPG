using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JasonRPG.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public const int DEFAULT_INVENTORY_SIZE = 29;

        public event Action<IItem> ItemEquipped;
        public event Action<IItem> ItemUnEquipped;
        
        [SerializeField] private Transform rightHand;
        [SerializeField] private Transform leftHand;

        private Item[] _items = new Item[DEFAULT_INVENTORY_SIZE];
        private Transform _itemRoot;

        public List<Item> Items => _items.ToList();
        public event Action<IItem> ActiveItemChanged;
        public event Action<Item> ItemPickedUp;
        public event Action<int> OnItemChanged;
        
        public IItem ActiveItem { get; private set; }
        public int Count => _items.Count(t => t != null);


        private void Awake()
        {
            _itemRoot = new GameObject("Items").transform;
            _itemRoot.SetParent(transform);
        }

        public void Pickup(Item item, int? slotPosition = null)
        {
            if (slotPosition.HasValue == false)
            {
                slotPosition = FindFirstAvailableSlot();
            }

            if (slotPosition.HasValue == false)
            {
                return;
            }
            _items[slotPosition.Value] = item;
            item.transform.SetParent(_itemRoot);

            ItemPickedUp?.Invoke(item);
            item.WasPickedUp = true;
            Equip(item);
        }

        private int? FindFirstAvailableSlot()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == null)
                    return i; 
            }

            return null; 
        }

        public void Equip(IItem item)
        {
            if (ActiveItem != null)
            {
                ActiveItem.transform.SetParent(_itemRoot);
                ActiveItem.gameObject.SetActive(false);
                ItemUnEquipped?.Invoke(ActiveItem);
            }
            Debug.Log($"Equipped Item: {item.gameObject.name}");
            
            item.transform.SetParent(rightHand);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            ActiveItem = item;
            ActiveItem.gameObject.SetActive(true);
            ActiveItemChanged?.Invoke(ActiveItem);
            ItemEquipped?.Invoke(item);
        }

        public Item GetItemInSlot(int slotPosition)
        {
            return _items[slotPosition];
        }

        public void Move(int sourceSlot, int destinationSlot)
        {
            var destinationItem = _items[destinationSlot];
            _items[destinationSlot] = _items[sourceSlot];
            _items[sourceSlot] = destinationItem;

            OnItemChanged?.Invoke(destinationSlot);
            OnItemChanged?.Invoke(sourceSlot);
        }
    }

}