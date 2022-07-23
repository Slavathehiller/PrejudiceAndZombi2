using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SectorSackCell : SackCell
{
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        var item = eventData.pointerDrag.GetComponent<ItemReference>();

        ((CharacterS)item.character).sack.RemoveItem(item);
        gameController.CurrentSector.sectorObject.AddItem(item);
        gameController.SectorItems.Add(item);
    }

    public void RemoveFromSector(ItemReference item)
    {
        gameController.RemoveFromCurrentSector(item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
