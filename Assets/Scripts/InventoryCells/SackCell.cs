using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SackCell : MonoBehaviour, IDropHandler
{
    public GameController gameController;
    public virtual void OnDrop(PointerEventData eventData)
    {        
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        var oldParentCell = item.oldParent.GetComponent<ItemCell>();
        if (oldParentCell == this)
            return;
        item.transform.SetParent(gameObject.transform);
        item.transform.localPosition = Vector3.zero;
        item.image.GetComponent<RectTransform>().sizeDelta = Item.defaultSize;
        var thing = item.thing.GetComponent<EquipmentItem>();
        if (!(thing is null))
        {
            thing.PlaceItemToSack(gameObject);
            item.character.inventory.UnEquipItem(thing.specType);
        }
        
        if (oldParentCell != null)
        {
            if (oldParentCell is RightHandCell)
                item.character.RemoveFromRightHand(false);
            item.character.inventory.TryRemoveItem(thing);
            oldParentCell.itemIn = null;
            oldParentCell.ShowBackground(true);
        }
        //item.ShowUnloadButton(false);
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
