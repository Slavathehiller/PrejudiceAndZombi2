using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class EquipmentCell : ItemCell
{
    public GameControllerS gameController;
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
            item.gameObject.transform.SetParent(transform);
            item.gameObject.transform.localPosition = Vector3.zero;            
            item.image.GetComponent<RectTransform>().sizeDelta = thing.sizeInInventory;

            ShowBackground(false);
            item.character.inventory.EquipItem(thing);
            PlaceItemToCell(thing);
            ((CharacterS)item.character).sack.RemoveItem(item);
            gameController._currentSector.sectorObject.RemoveItem(item);
            gameController.SectorItems.Remove(item);
        }
    }

    public virtual void PlaceItemToCell(EquipmentItem thing)
    {
        thing.itemRef.background.enabled = false;
    }

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerS>();
    }
}
