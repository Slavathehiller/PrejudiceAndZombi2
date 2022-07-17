using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class EquipmentCell : ItemCell
{

    public GameController gameController;

    public override void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        var thing = item.thing.GetComponent<EquipmentItem>();
        
        if (thing != null)
        {
            if (thing.specType != spec)
                return;
            var oldItem = item.character.inventory.getEquipmentItem(spec)?.itemRef;
            if (oldItem != null)
            {
                oldItem.thing.GetComponent<EquipmentItem>().PlaceItemToSack(item.oldParent);
            }

            ShowBackground(false);
            item.character.inventory.EquipItem(thing);
            PlaceItemToCell(item);
            ((CharacterS)item.character).sack.RemoveItem(item);
            gameController._currentSector.sectorObject.RemoveItem(item);
            gameController.SectorItems.Remove(item);
        }
    }

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
}
