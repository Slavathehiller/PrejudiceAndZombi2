using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMPistolCartridge : WeaponCartridge
{
    public HMPistolCartridge() 
    {
        capacity = 1;
        ammoTypeList = new List<AmmoType>() {AmmoType.FMG_9x18 };
        extractable = false;
        type = WeaponCartridgeType.HandmadePistolCartridge;
    }
}
