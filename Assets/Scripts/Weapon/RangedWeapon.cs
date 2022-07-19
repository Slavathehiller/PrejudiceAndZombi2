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
    public List<WeaponMagazineType> compatibleCartridgeTypes = new List<WeaponMagazineType>();

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

    public bool CanLoad(Ammo ammo)
    {
        return !magazine.extractable && magazine.AcceptableType(ammo.data.type);
    }

    public void Reload(Ammo ammo)
    {
        magazine.Reload(ammo);
        itemRef.ShowUnloadButton(true);
    }

    public bool CanLoad(WeaponMagazine weaponMagazine)
    {
        return (magazine == null || magazine.extractable) && compatibleCartridgeTypes.Contains(weaponMagazine.type);
    }

    public void Reload(WeaponMagazine weaponMagazine)
    {
        UnloadMagazine();
        magazine = weaponMagazine;
        magazine.itemRef.gameObject.SetActive(false);
        magazine.gameObject.SetActive(false);
        magazine.transform.SetParent(magazinePoint);
        magazine.transform.localPosition = Vector3.zero;
        //itemRef.character.inventory.TryRemoveItem(magazine.itemRef.thing.GetComponent<TacticalItem>());
        itemRef.ShowUnloadButton(true);
    }

    public void ConsumeAmmo(int num = 1)
    {
        magazine.ConsumeAmmo(num);
        itemRef.ShowUnloadButton(true);
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
            itemRef.ShowUnloadButton(false);
        }
    }

    protected override void Update()
    {
        RefreshAmmo();
    }
}
