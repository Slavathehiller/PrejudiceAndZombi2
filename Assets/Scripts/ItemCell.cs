using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour, IDropHandler
{
    

    public virtual void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        if (item != null)
        {
            item.character.pcontroller.PickUpItem(item);
            item.GetComponent<Image>().enabled = false;
            item.image.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
            item.transform.SetParent(gameObject.transform);
            item.transform.localPosition = Vector3.zero;
            item.thing.GetComponent<Light>().enabled = false;
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
