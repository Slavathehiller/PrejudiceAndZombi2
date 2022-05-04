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
    public Button unloadButton;
    public Text count;

    private bool backgroundState;
    private Image background;


    private void Start()
    {
        panel = GameObject.Find("InventoryPanel");
        canvasGroup = GetComponent<CanvasGroup>();
        background = GetComponent<Image>();        
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
        character.RemoveFromRightHand(false);
        ShowUnloadButton(false);
    }

    public void Unload()
    {
        var weapon = thing.GetComponent<TacticalItem>() as RangedWeapon;
        if(weapon != null && weapon.magazine != null)
        {
            if (weapon.magazine.extractable)
                weapon.UnloadMagazine();
            else
            {
                var ammoObject = Ammo.MakeObject(weapon.magazine.CurrentAmmoData);
                var ammo = ammoObject.GetComponent<TacticalItem>() as Ammo;
                if (ammo != null)
                {
                    ammoObject.transform.SetParent(thing.transform);
                    ammoObject.transform.localPosition = Vector3.zero;
                    ammo.SetCount(weapon.magazine.CurrentAmmoCount);
                    ammo.prefabController = character.pcontroller.prefabsController;
                    ammo.Drop();
                    weapon.magazine.CurrentAmmoCount = 0;
                    weapon.itemRef.ShowUnloadButton(false);
                }

            }
        }
    }

    public void ShowUnloadButton(bool on)
    {
        if (on)
        {
            var weapon = thing.GetComponent<TacticalItem>() as RangedWeapon;
            unloadButton.gameObject.SetActive(weapon != null && weapon.magazine != null && (weapon.magazine.extractable || weapon.magazine.CurrentAmmoCount > 0));
        }
        else
        {
            unloadButton.gameObject.SetActive(false);
        }
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
        backgroundState = background.enabled;
        background.enabled = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if(gameObject.transform.parent == panel.transform)
        {
            gameObject.transform.SetParent(oldParent.transform);
            gameObject.transform.localPosition = Vector3.zero;
            background.enabled = backgroundState;
        }
    }


}
