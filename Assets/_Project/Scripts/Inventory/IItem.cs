using System.Collections;
using UnityEngine;

namespace JasonRPG.Inventory
{
    public interface IItem
    {
        Sprite Icon { get; }
        GameObject gameObject { get; }
        Transform transform { get; }
        UseAction[] Actions { get; }
        StatMod[] StatMods { get; }
        CrosshairDefinition CrosshairDefinition { get; }
    }
}