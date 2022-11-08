using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interchange
{
    public class CharacterTransferData
    {
        public EntityStats Stats { get; set; }
        public string Name { get; set; }
        public Sprite Portrait { get; set; }
        public InventoryTransferData Inventory { get; set; }
        public ItemTransferData RightHand { get; set; }
        public IEnumerable<ItemTransferData> Sack { get; set; }
    }
}
