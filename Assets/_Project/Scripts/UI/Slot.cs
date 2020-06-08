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
        public Item Item { get; private set; }
        public bool IsEmplty => Item == null;
        public Sprite Icon => icon.sprite;

        public void SetItem(Item item)
        {
            Item = item;
            icon.sprite = item.Icon;
        }

       private void OnValidate()
       {
           _text = GetComponentInChildren<TMP_Text>();
           int hotkeyNumber = transform.GetSiblingIndex() + 1;
           gameObject.name = $"Slot {hotkeyNumber}";
           _text.SetText(hotkeyNumber.ToString());
       }
    }
}