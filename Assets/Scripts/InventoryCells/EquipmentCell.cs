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
                PlaceItemToSack(oldItem, item.oldParent);
            }
            item.gameObject.transform.SetParent(transform);
            item.gameObject.transform.localPosition = Vector3.zero;
            item.image.GetComponent<RectTransform>().sizeDelta = thing.sizeInInventory;
            PlaceItemToCell(thing);
            ShowBackground(false);
            item.character.inventory.EquipItem(thing);
            ((CharacterS)item.character).sack.RemoveItem(item);
            gameController._currentSector.sectorObject.RemoveItem(item);
            gameController.UnequipedItems.Remove(item);
        }
    }

    public virtual void PlaceItemToSack(ItemReference item, GameObject sack)
    {
        item.background.enabled = true;
        item.gameObject.transform.SetParent(sack.transform);
        item.image.GetComponent<RectTransform>().sizeDelta = Item.defaultSize;
    }
    public abstract void PlaceItemToCell(EquipmentItem thing);

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
}
