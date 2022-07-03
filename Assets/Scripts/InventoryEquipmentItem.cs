using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEquipmentItem : EquipmentItem
{
    public GameObject cellScheme;

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
