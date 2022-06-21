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
        var thing = item.thing.GetComponent<Item>();
        item.transform.SetParent(gameObject.transform);
        item.transform.localPosition = Vector3.zero;
        //item.image.GetComponent<RectTransform>().sizeDelta = thing.sizeInInventory;
        ((CharacterS)item.character).sack.RemoveItem(item);
        gameController._currentSector.sectorObject.AddItem(item);        
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
