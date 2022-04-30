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
    public List<WeaponMagazineType> compatibleCartridgeTypes = new List<WeaponMagazineType>();

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
        return magazine.extractable && compatibleCartridgeTypes.Contains(weaponMagazine.type);
    }

    public void Reload(WeaponMagazine weaponMagazine)
    {
        magazine.Drop();
        magazine = weaponMagazine;
        magazine.itemRef.gameObject.SetActive(false);
        magazine.gameObject.SetActive(false);
        magazine.transform.SetParent(magazinePoint);
        magazine.transform.localPosition = Vector3.zero;
       // Destroy(magazine.itemRef.gameObject);
    }

    public void ConsumeAmmo(int num = 1)
    {
        magazine.ConsumeAmmo(num);
    }

    public bool CanFire()
    {
        return magazine?.CurrentAmmoCount > 0;
    }

    void RefreshAmmo()
    {
        if (itemRef != null && magazine != null)
            itemRef.count.text = magazine.CurrentAmmoCount.ToString();
    }

    private void Update()
    {
        RefreshAmmo();
    }
}
