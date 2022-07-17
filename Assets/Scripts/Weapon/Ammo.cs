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
    public AudioClip shotSound;
}

public abstract class Ammo : TacticalItem
{
    public AudioClip shotSound;
    public AmmoData data = new AmmoData();
    protected override void Awake()
    {
        base.Awake();
        data.shotSound = shotSound;
    }

    public static GameObject MakeObject(AmmoData _data)
    {
        var result = Instantiate(_data.prefab);
        return result;
    }
}