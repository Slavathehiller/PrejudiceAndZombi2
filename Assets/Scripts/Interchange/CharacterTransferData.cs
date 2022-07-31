using System.Collections.Generic;

namespace Assets.Scripts.Interchange
{
    public class CharacterTransferData
    {
        public EntityStats Stats { get; set; }
        public InventoryTransferData Inventory { get; set; }
        public ItemTransferData RightHand { get; set; }
        public IEnumerable<ItemTransferData> Sack { get; set; }
    }
}
