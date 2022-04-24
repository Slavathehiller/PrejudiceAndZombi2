using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponCartridgeType
{
    HandmadePistolCartridge = 0
}

public class WeaponCartridge : MonoBehaviour
{
    public AmmoType CurrentAmmoType;
    public int CurrentAmmoCount;
    public List<AmmoType> ammoTypeList;
    public bool extractable;
    public int capacity;
    public WeaponCartridgeType type;

    public bool AcceptableType(AmmoType type)
    {
        return ammoTypeList.Contains(type);
    }
}
