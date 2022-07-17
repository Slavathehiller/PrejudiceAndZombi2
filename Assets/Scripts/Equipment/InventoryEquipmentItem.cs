using Assets.Scripts.Interchange;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryEquipmentItem : EquipmentItem
{
    public GameObject cellScheme;
    public List<ItemCell> cellList;

    public new EquipmentItemTransferData TransferData
    {
        get
        {
            var itemDataList = new List<ItemTransferData>();
            foreach(var cell in cellList)
            {
                if (cell.itemIn != null)
                    itemDataList.Add(cell.itemIn.TransferData);
                else
                    itemDataList.Add(null);
            }
            return new EquipmentItemTransferData
            {
                Prefab = this.prefab,
                ItemList = itemDataList
            };
        }
    }

    public List<TacticalItem> ItemList
    {
        get
        {
            var result = new List<TacticalItem>();
            foreach (var item in cellList)
            {
                result.Add(item.itemIn);
            }

            return result;
        }
    }

    public void TakeBackScheme()
    {
        cellScheme.transform.SetParent(gameObject.transform);
        cellScheme.SetActive(false);
    }

    public override void PlaceItemToSack(GameObject sack)
    {
        base.PlaceItemToSack(sack);
        ((InventoryEquipmentItem)GetComponent<EquipmentItem>()).TakeBackScheme();

    }


}
