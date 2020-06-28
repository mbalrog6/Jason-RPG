using System;
using UnityEngine;

namespace JasonRPG
{
    public interface IPlayerInput
    {
        event Action<int> HotkeyPressed;
        event Action<MovementMode> MovementSwitched;
        float Vertical { get; }
        float Horizontal { get; }
        float MouseX { get; }
        bool PausePressed { get; }
        Vector2 MousePosition { get; }
        void Tick();
        bool GetKeyDown(KeyCode keyCode);
    }
}