using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ItemReference : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
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
    public Button consumeButton;
    public Text count;

    private bool backgroundState;
    public Image background;

    private GameObject popup;

    public Item Item
    {
        get
        {
            if (item is null)
            {
                item = thing.GetComponent<Item>();
            }
            return item;
        }
    }

    private void Awake()
    {
        rootPanel = GameObject.Find("InventoryPanel");
        canvasGroup = GetComponent<CanvasGroup>();
        background = GetComponent<Image>();        
    }

    private void Start()
    {
        popup = Instantiate(Item.prefabsController.popup, transform);
        popup.GetComponent<PopupController>().Init(this, menuCommands);
    }

    protected virtual MenuPointData[] menuCommands
    {
        get
        {
            return new MenuPointData[1] {PopupController.CreateMenuPointData("Бросить", Drop, DropEnable) };
        }
    }

    private void Drop()
    {
        character.DropItem();
    }

    private bool DropEnable(ItemReference itemRef)
    {
        return character.RightHandItem == itemRef.item;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            popup.GetComponent<PopupController>().Popup(rootPanel);
            eventData.Reset();
        }
    }

    void Update()
    {        
        if (Item is RangedWeapon)
            (Item as RangedWeapon).RefreshAmmo();
        if (Item is WeaponMagazine)
            (Item as WeaponMagazine).RefreshAmmo();
    }

    private void OnDestroy()
    {
        if (character is CharacterS)
        {
            (character as CharacterS).gameController.RemoveFromCurrentSector(this);
            (character as CharacterS).gameController.RemoveFromPlayerSack(this);
        }
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
                    if (character is CharacterS)
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
