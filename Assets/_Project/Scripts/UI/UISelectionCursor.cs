using System;
using JasonRPG;
using UnityEngine;
using UnityEngine.UI;

public class UISelectionCursor : MonoBehaviour
{
    [SerializeField] private Image image;
    private UIInventoryPanel _inventoryPanel;
    public bool IconVisable => image != null && image.enabled && image.sprite != null;
    public Sprite Icon => image.sprite;

    private void Awake()
    {
        _inventoryPanel = FindObjectOfType<UIInventoryPanel>();
        image.enabled = false;
        image.raycastTarget = false; 
    }

    private void Update()
    {
        transform.position = PlayerInput.Instance.MousePosition;
    }

    private void OnEnable() => _inventoryPanel.OnSelectionChanged += Handle_SelectionChanged;
    private void OnDisable() => _inventoryPanel.OnSelectionChanged -= Handle_SelectionChanged;

    private void Handle_SelectionChanged()
    {
        image.sprite = _inventoryPanel.Selected ? _inventoryPanel.Selected.Icon : null;
        image.enabled = image.sprite != null;
    }
}