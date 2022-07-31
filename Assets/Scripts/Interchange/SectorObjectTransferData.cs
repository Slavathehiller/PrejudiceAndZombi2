using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interchange
{
    public class SectorObjectTransferData
    {
        public GameObject prefab;
        public float findChance;
        public IEnumerable<ItemTransferData> sack;

        public SectorObject Restore()
        {
            var obj = GameObject.Instantiate(prefab).GetComponent<SectorObject>();
            obj.findChance = findChance;
            return obj;
        }
    }
}
