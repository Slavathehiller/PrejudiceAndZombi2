using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightHandCell : ItemCell
{
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        if (item != null && item.thing.GetComponent<TacticalItem>().size <= size)
        {
            item.character.pcontroller.PickUpItem(item);
        }
    }
}
