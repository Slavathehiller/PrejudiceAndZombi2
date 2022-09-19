using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interchange
{
    public class EquipmentItemTransferData : ItemTransferData
    {
        public List<ItemTransferData> ItemList { get; set; }

        public override Item Restore(GameObject parent = null, ICharacter character = null)
        {
            var obj = base.Restore(parent, character);
            var item = obj.GetComponent<InventoryEquipmentItem>();

            var i = 0;
            foreach (var itemdata in ItemList)
            {
                if (itemdata != null)
                {
                    var tac_item = itemdata.Restore(parent, character);
                    item.cellList[i].PlaceItemToCell(tac_item);
                    //if (tac_item is RangedWeapon)
                    //    tac_item.itemRef.ShowUnloadButton(false);
                }
                i++;
            }

            item.itemRef.gameObject.SetActive(character is CharacterS);
            return item;
        }
    }
}
