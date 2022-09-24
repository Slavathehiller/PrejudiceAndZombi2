using Assets.Scripts.Entity;
using Assets.Scripts.Interchange;
using Assets.Scripts.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : BaseWeapon
{
    public RangedAttackModifier rangedAttackModifier = new RangedAttackModifier();
    public float ShootCost;
    public GameObject bulletSpawner;
    public WeaponMagazine magazine;
    public Transform magazinePoint;
    public Sprite imageWithoutMagazine;
    public Sprite imageWithMagazine;
    public GameObject magazineModel;
    public List<WeaponMagazineType> compatibleMagazineTypes = new List<WeaponMagazineType>();

    public override ItemTransferData TransferData
    {
        get
        {
            return new RangedWeaponTransferData
            {
                Prefab = this.prefab,
                Magazine = this.magazine is null ? null : this.magazine.TransferData as WeaponMagazineTransferData
            };
        }
    }

    public override List<MenuPointData> menuCommands
    {
        get
        {
            var result = base.menuCommands;
            result.Add(PopupController.CreateMenuPointData("Разрядить", Unload, UnloadEnabled));
            return result;
        }
    }

    private void Unload()
    {
        if (magazine != null)
        {
            if (magazine.extractable)
            {
                if (itemRef.character is CharacterS)
                {
                    (itemRef.character as CharacterS).gameController.AddItemToPlayerSack(magazine.itemRef);
                    magazine.gameObject.SetActive(false);
                }
                UnloadMagazine();
            }
            else
            {
                var ammoObject = Ammo.MakeObject(magazine.CurrentAmmoData);
                var ammo = ammoObject.GetComponent<TacticalItem>() as Ammo;
                if (ammo != null)
                {
                    if (itemRef.character is CharacterS)
                    {
                        (itemRef.character as CharacterS).gameController.AddItemToPlayerSack(ammo.itemRef);
                        ammo.gameObject.SetActive(false);
                    }
                    else
                    {
                        ammoObject.transform.SetParent(transform);
                        ammoObject.transform.localPosition = Vector3.zero;
                        ammo.Drop();
                    }
                    ammo.SetCount(magazine.CurrentAmmoCount);
                    magazine.CurrentAmmoCount = 0;
                }
            }
        }
    }

    private bool UnloadEnabled(Item item)
    {        
        return (itemRef.character is CharacterS || itemRef.character.RightHandItem == item) && magazine != null && (magazine.extractable || magazine.CurrentAmmoCount > 0);
    }

    public bool CanLoad(Ammo ammo)
    {
        return !magazine.extractable && magazine.AcceptableType(ammo.data.type);
    }

    public void Reload(Ammo ammo)
    {
        magazine.Reload(ammo);
    }

    public bool CanLoad(WeaponMagazine weaponMagazine)
    {
        return (magazine == null || magazine.extractable) && compatibleMagazineTypes.Contains(weaponMagazine.type);
    }

    public void Reload(WeaponMagazine weaponMagazine)
    {
        var character = weaponMagazine.itemRef.character;
        if (character is CharacterS)
        {
            (character as CharacterS).gameController.RemoveFromCurrentSector(weaponMagazine.itemRef);
            (character as CharacterS).gameController.RemoveFromPlayerSack(weaponMagazine.itemRef);
        }
        UnloadMagazine();
        magazine = weaponMagazine;
        magazine.itemRef.gameObject.SetActive(false);
        magazine.gameObject.SetActive(false);
        magazine.transform.SetParent(magazinePoint);
        magazine.transform.localPosition = Vector3.zero;
    }

    public void ConsumeAmmo(int num = 1)
    {
        magazine.ConsumeAmmo(num);
    }

    public bool CanFire()
    {
        return magazine?.CurrentAmmoCount > 0;
    }

    public void RefreshAmmo()
    {
        if (magazine is null)
        {
            itemRef.count.text = "-";
        }

        if (itemRef != null && magazine != null)
            itemRef.count.text = magazine.CurrentAmmoCount.ToString();
        if (magazineModel != null)
            magazineModel.SetActive(magazine != null);
        if (itemRef != null)
        {
            if (magazine is null)
                itemRef.image.sprite = imageWithoutMagazine;
            else
                itemRef.image.sprite = imageWithMagazine;
        }
    }

    public void UnloadMagazine()
    {
        if(magazine != null)
        {
            if (itemRef.character is CharacterS)
                (itemRef.character as CharacterS).gameController.AddItemToPlayerSack(magazine.itemRef);
            else
                magazine.Drop();
            magazine = null;
        }
    }

    private void Start()
    {
        magazine?.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        RefreshAmmo();
    }
}
