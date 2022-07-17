using Assets.Scripts.Interchange;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponMagazineType
{
    HomemadePistolMagazine = 0,
    HomemadeSMGMagazine = 1

}

public class WeaponMagazine : TacticalItem
{
    public AmmoData CurrentAmmoData;
    public int CurrentAmmoCount;
    public List<AmmoType> ammoTypeList;
    public bool extractable;
    public int capacity;
    public WeaponMagazineType type;

    public new WeaponMagazineTransferData TransferData
    {
        get
        {
            return new WeaponMagazineTransferData
            {
                Prefab = this.prefab,
                CurrentAmmoCount = this.CurrentAmmoCount,
                CurrentAmmoData = this.CurrentAmmoData
            };
        }
    }
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
        var loadCount = Mathf.Min(freeSlots, ammo.GetCount());
        CurrentAmmoData = ammo.data;
        CurrentAmmoCount += loadCount;
        ammo.Add(-loadCount);
    }

    void RefreshAmmo()
    {
        if (itemRef != null)
            itemRef.count.text = CurrentAmmoCount.ToString();
    }

    protected override void Update()
    {
        RefreshAmmo();
    }
}
