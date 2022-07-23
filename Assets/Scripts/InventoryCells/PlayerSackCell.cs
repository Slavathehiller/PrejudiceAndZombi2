using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSackCell : SackCell
{
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        var item = eventData.pointerDrag.GetComponent<ItemReference>();

        ((CharacterS)item.character).sack.AddItem(item);
        gameController.CurrentSector.sectorObject.RemoveItem(item);
        gameController.SectorItems.Remove(item);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
