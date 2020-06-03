using UnityEngine;
using UnityEngine.UI;


namespace JasonRPG.UI
{
    using Inventory;

    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private CrosshairDefinition invalidCrosshairDefinition;

        private Inventory _inventory;
        private Image _crosshairImage;

        private void OnEnable()
        {
            _inventory = FindObjectOfType<Inventory>();
            _inventory.ActiveItemChanged += Handle_ActiveItemChanged;
            if (_inventory.ActiveItem != null)
            {
                Handle_ActiveItemChanged(_inventory.ActiveItem);
            }
            else
            {
                _crosshairImage.sprite = invalidCrosshairDefinition.Sprite;
            }
            
        }

        private void OnValidate()
        {
            _crosshairImage = GetComponent<Image>();
        }

        public void Handle_ActiveItemChanged( Item activeItem )
        {
            if (activeItem != null && activeItem.CrosshairDefinition != null)
            {
                _crosshairImage.sprite = activeItem.CrosshairDefinition.Sprite;
                Debug.Log( $"Crosshair detected {activeItem.CrosshairDefinition}");
            }
            else
            {
                _crosshairImage.sprite = invalidCrosshairDefinition.Sprite;
            }

            
        }
    }
    

}