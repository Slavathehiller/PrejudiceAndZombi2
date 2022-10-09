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
    public override string StatsInfo
    {
        get
        {
            string result = $"Урон: {data.BaseDamage}\n";
            if (data.attackModifier.ToHitModifier > 0)
                result += $"+ {data.attackModifier.ToHitModifier} к точности\n";
            if (data.attackModifier.CritModifier > 0)
                result += $"+ {data.attackModifier.CritModifier} к шансу критического урона\n";
            return result;
        }
    }

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