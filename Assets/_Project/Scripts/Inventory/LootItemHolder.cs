using System;
using JasonRPG.Inventory;
using UnityEngine;

namespace JasonRPG
{
   public class LootItemHolder : MonoBehaviour
   {
      [SerializeField] private Transform itemPosition;
      [SerializeField] private float rotationSpeed = 5f;
      
      private Item _item;

      public void TakeItem(Item item)
      {
         _item = item;
         _item.transform.SetParent(itemPosition);
         _item.transform.localPosition = Vector3.zero; 
         _item.transform.localRotation = Quaternion.identity;
         _item.gameObject.SetActive(true);
         _item.WasPickedUp = false;
         _item.OnPickedUp += HandleItemPickedUp;
      }

      private void HandleItemPickedUp()
      {
         LootSystem.AddToPool(this);
      }

      private void Update()
      {
         var amount = Time.deltaTime * rotationSpeed;
         itemPosition.Rotate(0, amount, 0);
      }
   }
}
