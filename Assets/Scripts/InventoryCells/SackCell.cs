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
        item.transform.SetParent(gameObject.transform);
        item.transform.localPosition = Vector3.zero;
        item.image.GetComponent<RectTransform>().sizeDelta = Item.defaultSize;
        var thing = item.thing.GetComponent<EquipmentItem>();
        if (!(thing is null))
        {
            thing.PlaceItemToSack(gameObject);
        }
        var oldParentCell = item.oldParent.GetComponent<ItemCell>();
        if (oldParentCell != null)
        {
            oldParentCell.itemIn = null;
            oldParentCell.ShowBackground(true);
        }
        item.ShowUnloadButton(false);
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
