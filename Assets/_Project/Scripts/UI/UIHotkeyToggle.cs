using System;
using JasonRPG;
using UnityEngine;

public class UIHotkeyToggle : MonoBehaviour
{
  [SerializeField] private KeyCode keyCode = KeyCode.I;
  [SerializeField] private GameObject _gameObjectToToggle;
  private void Update()
  {
    if (PlayerInput.Instance.GetKeyDown(keyCode))
    {
      _gameObjectToToggle.SetActive(!_gameObjectToToggle.activeSelf);
    }
  }
}
