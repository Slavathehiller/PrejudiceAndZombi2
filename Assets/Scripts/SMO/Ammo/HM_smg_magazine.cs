using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_smg_magazine : WeaponMagazine
{
    public override string Caliber => "9x18 мм";

    protected override void Awake()
    {
        base.Awake();
        Name = "Магазин для самодельного ПП";
        capacity = 30;
        ammoTypeList = new List<AmmoType>() { AmmoType.FMG_9x18 };
        extractable = true;
        type = WeaponMagazineType.HomemadeSMGMagazine;        
        prefab = prefabsController.homemade_SMG_magazine;
        Description = $"Магазин для самодельного ПП. Вмещает {capacity} патронов 9х18 мм.";
    }
}
