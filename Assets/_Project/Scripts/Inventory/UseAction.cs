using System;

namespace JasonRPG.Inventory
{
    [Serializable]
    public struct UseAction
    {
        public UseMode UseMode;
        public ItemComponent TargetComponent;
    }
}