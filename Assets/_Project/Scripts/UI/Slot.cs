using System;
using JasonRPG.Inventory;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace JasonRPG.UI
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Image icon;
        private TMP_Text _text;
        private UIInventorySlot _inventorySlot;
        public IItem Item => _inventorySlot.Item;
        public bool IsEmplty => Item == null;
        public Sprite Icon => icon.sprite;

        private void Awake() => _inventorySlot = GetComponent<UIInventorySlot>();

        private void OnValidate()
       {
           _text = GetComponentInChildren<TMP_Text>();
           int hotkeyNumber = transform.GetSiblingIndex() + 1;
           gameObject.name = $"Slot {hotkeyNumber}";
           _text.SetText(hotkeyNumber.ToString());
       }
    }
}