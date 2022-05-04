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
            var rightHandItem = item.character.RightHandItem;

            var weapon = rightHandItem as RangedWeapon;
            if (thing is Ammo && weapon != null && weapon.CanLoad(thing as Ammo))
            {
                weapon.Reload(thing as Ammo);
            }
            if (thing is WeaponMagazine && weapon != null && weapon.CanLoad(thing as WeaponMagazine))
            {
                weapon.Reload(thing as WeaponMagazine);
            }
            var magazine = rightHandItem as WeaponMagazine;
            if (thing is Ammo && magazine != null && magazine.AcceptableType((thing as Ammo).data.type))
            {
                magazine.Reload(thing as Ammo);
            }


        }        

        if (TryToDrop(eventData))
        {
            item = eventData.pointerDrag.GetComponent<ItemReference>();
            if (item != null)
            {
                item.character.pcontroller.PickUpItem(item);
                var _thing = item.thing.GetComponent<RangedWeapon>();
                if (_thing != null)
                {
                    _thing.itemRef.ShowUnloadButton(true);
                }
            }
        }
    }
}
