using Assets.Scripts.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : BaseWeapon
{
    public RangedAttackModifier rangedAttackModifier = new RangedAttackModifier();
    public float ShootCost;
    public GameObject bulletSpawner;
    public WeaponCartridge cartridge;
    public List<WeaponCartridgeType> compatibleCartridgeTypes = new List<WeaponCartridgeType>();

    public void Reload(Ammo ammo)
    {
        var freeSlots = cartridge.capacity - cartridge.CurrentAmmoCount;
        var loadCount = Mathf.Min(freeSlots, ammo.Count);
        cartridge.CurrentAmmoData = ammo.data;
        cartridge.CurrentAmmoCount += (loadCount);
        ammo.Add(-loadCount);
    }

    public void ConsumeAmmo(int num = 1)
    {
        cartridge.ConsumeAmmo(num);
    }

    public bool CanFire()
    {
        return cartridge?.CurrentAmmoCount > 0;
    }

    public bool CanLoad(Ammo ammo)
    {
        return !cartridge.extractable && cartridge.AcceptableType(ammo.data.type);
    }

    void RefreshAmmo()
    {
        if (itemRef != null && cartridge != null)
            itemRef.count.text = cartridge.CurrentAmmoCount.ToString();
    }

    private void Update()
    {
        RefreshAmmo();
    }
}
