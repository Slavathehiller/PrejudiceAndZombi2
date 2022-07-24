using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interchange
{
    public class ItemTransferData
    {
        public GameObject Prefab { get; set; }

        public int Count { get; set; }

        public virtual Item Restore(ICharacter character)
        {
            var obj = UnityEngine.Object.Instantiate(Prefab);
            var item = obj.GetComponent<Item>();
            item.SetCount(Count);
            item.itemRef.character = character;
            return item;
        }
    }
}
