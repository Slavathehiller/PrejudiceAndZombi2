using UnityEngine.EventSystems;

public class SectorSackCell : SackCell
{
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        var oldParentCell = item.oldParent.GetComponent<ItemCell>();
        if (oldParentCell == this)
            return;

        ((CharacterS)item.character).sack.RemoveItem(item);
        gameController.CurrentSector.sectorObject.AddItem(item);
        if(!gameController.SectorItems.Contains(item))
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
