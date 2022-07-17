using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interchange
{
    public class InventoryTransferData
    {
        public EquipmentItemTransferData shirt { get; set; }
        public EquipmentItemTransferData pants { get; set; }
        public EquipmentItemTransferData belt { get; set; }

        public ItemTransferData helmet;
        public ItemTransferData chestArmor;
        public ItemTransferData gloves;
        public ItemTransferData boots;

    }
}
