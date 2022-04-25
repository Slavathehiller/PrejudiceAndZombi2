using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RangedAttackModifier
{
    public float ToHitModifier;
    public float CritModifier;

    public RangedAttackModifier(float toHit = 0, float crit = 0)
    {
        ToHitModifier = toHit;
        CritModifier = crit;
    }
}
