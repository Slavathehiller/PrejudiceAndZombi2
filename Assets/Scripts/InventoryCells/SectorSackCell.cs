using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SectorSackCell : MonoBehaviour, IDropHandler
{
    public GameController gameController;
    public void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        item.transform.SetParent(gameObject.transform);
        item.transform.localPosition = Vector3.zero;
        item.image.GetComponent<RectTransform>().sizeDelta = Item.defaultSize;
        ((CharacterS)item.character).sack.RemoveItem(item);
        gameController._currentSector.sectorObject.AddItem(item);
        item.character.inventory.TryRemoveItem(item.thing.GetComponent<Item>());
        var oldParentCell = item.oldParent.GetComponent<ItemCell>();
        var thing = item.thing.GetComponent<EquipmentItem>();
        if (!(thing is null))
        {
            thing.PlaceItemToSack(gameObject);
            gameController.RefreshArmorText();
        }
        if (oldParentCell != null)
            oldParentCell.ShowBackground(true);
        gameController.SectorItems.Add(item);
    }

    public void RemoveFromSector(ItemReference item)
    {
        gameController.SectorItems.Remove(item);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
