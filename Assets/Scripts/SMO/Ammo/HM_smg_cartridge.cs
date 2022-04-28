using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_smg_cartridge : WeaponCartridge
{
    public HM_smg_cartridge()
    {
        capacity = 30;
        ammoTypeList = new List<AmmoType>() { AmmoType.FMG_9x18 };
        extractable = true;
        type = WeaponCartridgeType.HandmadeSMGCartridge;
    }
}
