using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType
{
    None = 0,
    FMG_9x18 = 1
}

public struct AmmoData
{
    public AmmoType type;
    public RangedAttackModifier attackModifier;
    public float BaseDamage;
    public GameObject prefab;
}

public abstract class Ammo : SMO
{
    public PrefabsController prefabController;
    public AmmoData data = new AmmoData();

    public static GameObject MakeObject(AmmoData _data)
    {
        var result = Instantiate(_data.prefab);
        return result;
    }
}