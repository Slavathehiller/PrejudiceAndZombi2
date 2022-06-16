using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_smg_magazine : WeaponMagazine
{
    public HM_smg_magazine()
    {
        Name = "Магазин для самодельного ПП";
        capacity = 30;
        ammoTypeList = new List<AmmoType>() { AmmoType.FMG_9x18 };
        extractable = true;
        type = WeaponMagazineType.HomemadeSMGMagazine;
    }
}
