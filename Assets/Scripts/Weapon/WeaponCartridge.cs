using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponCartridgeType
{
    HandmadePistolCartridge = 0,
    HandmadeSMGCartridge = 1

}

public class WeaponCartridge : TacticalItem
{
    public AmmoData CurrentAmmoData;
    public int CurrentAmmoCount;
    public List<AmmoType> ammoTypeList;
    public bool extractable;
    public int capacity;
    public WeaponCartridgeType type;

    public bool AcceptableType(AmmoType type)
    {
        return ammoTypeList.Contains(type);
    }

    public void ConsumeAmmo(int num = 1)
    {
        CurrentAmmoCount -= num;
        if (CurrentAmmoCount < 1)
            CurrentAmmoData.type = AmmoType.None;
    }

    public void Reload(Ammo ammo)
    {
        var freeSlots = capacity - CurrentAmmoCount;
        var loadCount = Mathf.Min(freeSlots, ammo.Count);
        CurrentAmmoData = ammo.data;
        CurrentAmmoCount += loadCount;
        ammo.Add(-loadCount);
    }

    void RefreshAmmo()
    {
        if (itemRef != null)
            itemRef.count.text = CurrentAmmoCount.ToString();
    }

    private void Update()
    {
        RefreshAmmo();
    }
}
