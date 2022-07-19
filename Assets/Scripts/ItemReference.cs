using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemReference : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject thing;
    public Item item;
    public Image image;
    public ICharacter character;
    [HideInInspector]
    public GameObject rootPanel;
    public CanvasGroup canvasGroup;
    public GameObject oldParent;
    public Button unloadButton;
    public Text count;

    private bool backgroundState;
    public Image background;



    private void Awake()
    {
        rootPanel = GameObject.Find("InventoryPanel");
        canvasGroup = GetComponent<CanvasGroup>();
        background = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {        
        if (thing != null && item is null)
            item = thing.GetComponent<Item>();
        if (item is RangedWeapon)
        {
            (item as RangedWeapon).RefreshAmmo();
            //var magazine = (item as RangedWeapon).magazine;
            //if (magazine != null)
            //    count.text = magazine.CurrentAmmoCount.ToString();
            //else
            //    count.text = "-";
        }
        if (item is WeaponMagazine)
            (item as WeaponMagazine).RefreshAmmo();
    }
    public void PickUp()
    {
        character.PickUpItem(this);
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
            {
                if (character is CharacterS)
                {
                    (character as CharacterS).gameController.AddItemToPlayerSack(weapon.magazine.itemRef);
                    weapon.magazine.gameObject.SetActive(false);
                }
                weapon.UnloadMagazine();
            }
            else
            {
                var ammoObject = Ammo.MakeObject(weapon.magazine.CurrentAmmoData);
                var ammo = ammoObject.GetComponent<TacticalItem>() as Ammo;
                if (ammo != null)
                {
                    if ((item as RangedWeapon).magazine)
                    {
                        (character as CharacterS).gameController.AddItemToPlayerSack(ammo.itemRef);
                        ammo.gameObject.SetActive(false);
                    }
                    else
                    {
                        ammoObject.transform.SetParent(thing.transform);
                        ammoObject.transform.localPosition = Vector3.zero;
                        ammo.Drop();
                    }
                    ammo.SetCount(weapon.magazine.CurrentAmmoCount);
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
        gameObject.transform.SetParent(rootPanel.transform);
        canvasGroup.blocksRaycasts = false;
        backgroundState = background.enabled;
        background.enabled = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if(gameObject.transform.parent == rootPanel.transform)
        {
            gameObject.transform.SetParent(oldParent.transform);
            gameObject.transform.localPosition = Vector3.zero;
            background.enabled = backgroundState;
        }
        if (gameObject.transform.parent.GetComponent<PlayerSackCell>() != null || gameObject.transform.parent.GetComponent<SectorSackCell>() != null)
            background.enabled = true;
    }


}
