using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentCell : ItemCell
{
    public GameObject container;
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
                oldItem.gameObject.transform.SetParent(item.oldParent.transform);
                oldItem.thing.GetComponent<EquipmentItem>().TakeBackScheme();
                oldItem.image.GetComponent<RectTransform>().sizeDelta = Item.defaultSize;
                oldItem.background.enabled = true;
            }
            item.gameObject.transform.SetParent(transform);
            item.gameObject.transform.localPosition = Vector3.zero;
            item.image.GetComponent<RectTransform>().sizeDelta = thing.sizeInInventory;
            thing.cellScheme.transform.SetParent(container.transform);
            thing.cellScheme.transform.localPosition = Vector3.zero;
            thing.cellScheme.SetActive(true);
            ShowBackground(false);
            item.character.inventory.EquipItem(thing);
            ((CharacterS)item.character).sack.RemoveItem(item);
            gameController._currentSector.sectorObject.RemoveItem(item);
            gameController.UnequipedItems.Remove(item);
        }
    }
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
}
