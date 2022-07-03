using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    public SpecType specType;

    public virtual void PlaceItemToSack(GameObject sack)
    {
        itemRef.background.enabled = true;
        itemRef.gameObject.transform.SetParent(sack.transform);
        itemRef.image.GetComponent<RectTransform>().sizeDelta = Item.defaultSize;
    }
}
