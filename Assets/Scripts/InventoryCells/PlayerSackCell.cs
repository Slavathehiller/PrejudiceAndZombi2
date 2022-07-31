using UnityEngine.EventSystems;

public class PlayerSackCell : SackCell
{
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        var oldParentCell = item.oldParent.GetComponent<ItemCell>();
        if (oldParentCell == this)
            return;
        ((CharacterS)item.character).sack.AddItem(item);
        gameController.CurrentSector.sectorObject.RemoveItem(item);
        gameController.SectorItems.Remove(item);
    }

    public void RemoveFromPlayerSack(ItemReference item)
    {
        ((CharacterS)item.character).sack.RemoveItem(item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
