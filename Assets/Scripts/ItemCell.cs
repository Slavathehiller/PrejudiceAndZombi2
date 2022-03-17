using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCell : MonoBehaviour, IDropHandler
{
    

    public virtual void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.SetParent(gameObject.transform);
        eventData.pointerDrag.transform.localPosition = Vector3.zero;
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
