using UnityEngine;

namespace JasonRPG.UI
{

    public class Hotbar : MonoBehaviour
    {
        private Inventory.Inventory _inventory;
        private Slot[] _slots;

        private void OnEnable()
        {
            PlayerInput.Instance.HotkeyPressed += Handle_HotkeyPressed;
            _inventory = FindObjectOfType<Inventory.Inventory>();
            _slots = GetComponentsInChildren<Slot>();
        }

        private void OnDisable()
        {
            PlayerInput.Instance.HotkeyPressed -= Handle_HotkeyPressed;
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
        
    }
}


