using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightHandCell : ItemCell
{
    public override void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        if (item != null)
        {
            var thing = item.thing.GetComponent<TacticalItem>();
            var weapon = item.character.RightHandItem as RangedWeapon;
            if (thing is Ammo && weapon != null && weapon.CanLoad(thing as Ammo))
            {
                weapon.Reload(thing as Ammo);
            }
        }        

        if (TryToDrop(eventData))
        {
            item = eventData.pointerDrag.GetComponent<ItemReference>();
            if (item != null)
            {
                item.character.pcontroller.PickUpItem(item);
            }
        }
    }
}
