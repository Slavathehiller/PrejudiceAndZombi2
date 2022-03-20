using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemReference : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject thing;
    public Image image;
    [HideInInspector]
    public Character character;
    [HideInInspector]
    public GameObject panel;
    public CanvasGroup canvasGroup;
    public GameObject oldParent;
    

    private void Start()
    {
        panel = GameObject.Find("InventoryPanel");
        canvasGroup = GetComponent<CanvasGroup>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void PickUp()
    {
        character.pcontroller.PickUpItem(this);
    }

    public void RemoveFromRightHand()
    {
        character.pcontroller.RemoveFromRightHand();
    }

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.position = eventData.position;
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        oldParent = gameObject.transform.parent.gameObject;
        gameObject.transform.SetParent(panel.transform);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if(gameObject.transform.parent == panel.transform)
        {
            gameObject.transform.SetParent(oldParent.transform);
            gameObject.transform.localPosition = Vector3.zero;
        }
    }
}
