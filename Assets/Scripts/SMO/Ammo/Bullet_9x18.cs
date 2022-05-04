using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_9x18 : Ammo
{
    protected override void Awake()
    {
        base.Awake();
        _maxAmount = 50;
        data.type = AmmoType.FMG_9x18;
        data.BaseDamage = 10f;
        data.attackModifier = new RangedAttackModifier();
        data.prefab = prefabController.Bullet_9x18;
    }
}
