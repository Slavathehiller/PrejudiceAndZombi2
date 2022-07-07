using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_9x18 : Ammo
{
    protected override void Awake()
    {
        _maxAmount = 50;
        Name = "Патроны 9x18 мм";
        data.type = AmmoType.FMG_9x18;
        data.BaseDamage = 10f;
        data.attackModifier = new RangedAttackModifier();
        data.prefab = prefabController.Bullet_9x18;
        base.Awake();
    }
}
