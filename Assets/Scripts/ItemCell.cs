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

    public SpecType spec = SpecType.Universal;

    public GameObject background;
    public virtual void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        var thing = item.thing.GetComponent<TacticalItem>();
        if (item != null && thing.size <= size && (spec == SpecType.Universal || spec == thing.spec))
        {           
            item.GetComponent<Image>().enabled = false;
            
            item.transform.SetParent(gameObject.transform);
            item.transform.localPosition = Vector3.zero;
            item.thing.GetComponent<Light>().enabled = false;
            var oldParent = item.oldParent.GetComponent<ItemCell>();
            if (oldParent is RightHandCell)
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
            if(oldParent != null)
                oldParent.ShowBackground(true);
            ShowBackground(false);
        }
        
    }

    public void ShowBackground(bool on)
    {
        if(background != null)
        {
            background.SetActive(on);
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
