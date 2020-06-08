using System;

namespace JasonRPG
{
    public interface IPlayerInput
    {
        event Action<int> HotkeyPressed;
        event Action<MovementMode> MovementSwitched;
        float Vertical { get; }
        float Horizontal { get; }
        float MouseX { get; }
        void Tick();
    }
}