using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interchange
{
    public class EquipmentItemTransferData
    {
        public GameObject Prefab { get; set; }
        public List<ItemTransferData> ItemList { get; set; }
    }
}
