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
}

public class Ammo : SMO
{
    public AmmoData data = new AmmoData();
}
