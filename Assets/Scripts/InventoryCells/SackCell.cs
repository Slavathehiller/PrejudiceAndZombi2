using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SackCell : MonoBehaviour, IDropHandler
{
    public GameControllerS gameController;

    public virtual void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        item.transform.SetParent(gameObject.transform);
        item.transform.localPosition = Vector3.zero;
        item.image.GetComponent<RectTransform>().sizeDelta = Item.defaultSize;
        item.character.inventory.TryRemoveItem(item.thing.GetComponent<Item>());
        var thing = item.thing.GetComponent<EquipmentItem>();
        if (!(thing is null))
        {
            thing.PlaceItemToSack(gameObject);
            gameController.RefreshArmorText();
        }
        var oldParentCell = item.oldParent.GetComponent<ItemCell>();
        if (oldParentCell != null)
            oldParentCell.ShowBackground(true);
    }




    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerS>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
