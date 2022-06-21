using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SackCell : MonoBehaviour, IDropHandler
{
    public GameController gameController;
    public void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemReference>();
        var thing = item.thing.GetComponent<Item>();
        item.transform.SetParent(gameObject.transform);
        item.transform.localPosition = Vector3.zero;
        //item.image.GetComponent<RectTransform>().sizeDelta = thing.sizeInInventory;
        gameController._currentSector.sectorObject.RemoveItem(item);
        ((CharacterS)item.character).sack.AddItem(item);
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
