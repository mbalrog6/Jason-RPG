using System;
using JasonRPG.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IPointerDownHandler, IEndDragHandler, IDragHandler,
    IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Image selectedImage;
    [SerializeField] private Image focusedImage;
    [SerializeField] private int _sortIndex;

    public event Action<UIInventorySlot> OnSlotClicked;

    public bool IsEmpty => Item == null;

    public Sprite Icon => image.sprite;

    public IItem Item { get; private set; }

    public bool IconImageEnabled => image.enabled;

    public int SortIndex => _sortIndex;

    public void SetItem(IItem item)
    {
        Item = item;
        image.sprite = item != null ? item.Icon : null;
        image.enabled = item != null;
    }

    public void Clear()
    {
        Item = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSlotClicked?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var droppedOnSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<UIInventorySlot>();
        if (droppedOnSlot != null)
        {
            droppedOnSlot.OnPointerDown(eventData);
        }
        else
        {
            OnPointerDown(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData) { }

    public void BecomesSelected()
    {
        if (selectedImage)
        {
            selectedImage.enabled = true;
        }
    }
    
    public void BecomesUnSelected()
    {
        if (selectedImage)
        {
            selectedImage.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (focusedImage != null)
            focusedImage.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) => DisableFocusedImage();

    private void OnDisable() => DisableFocusedImage();

    private void DisableFocusedImage()
    {
        if (focusedImage != null)
            focusedImage.enabled = false;
    }
}