using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMPistolMagazine : WeaponMagazine
{
    public override string Caliber => "9x18 μμ";

    protected override void Awake()
    {
        base.Awake();
        capacity = 1;
        ammoTypeList = new List<AmmoType>() {AmmoType.FMG_9x18 };
        extractable = false;
        type = WeaponMagazineType.HomemadePistolMagazine;
        prefab = prefabsController.homemade_pistol_magazine;
    }
}
