using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum CellSize{
    Small = 0,
    Medium = 1,
    Large = 2
}
public class ItemCell : MonoBehaviour, IDropHandler
{
    public CellSize size;

    public virtual void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        var thing = item.thing.GetComponent<TacticalItem>();
        if (item != null && thing.size <= size)
        {           
            item.GetComponent<Image>().enabled = false;
            
            item.transform.SetParent(gameObject.transform);
            item.transform.localPosition = Vector3.zero;
            item.thing.GetComponent<Light>().enabled = false;
            if (item.oldParent.GetComponent<ItemCell>() is RightHandCell)
            {
                item.RemoveFromRightHand();
            }
            if (!(this is RightHandCell))
            {
                item.thing.SetActive(false);
                item.image.GetComponent<RectTransform>().sizeDelta = thing.sizeInInventory;
            }
            else
                item.image.GetComponent<RectTransform>().sizeDelta = thing.sizeInHand;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
