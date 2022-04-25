using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_9x18 : Ammo
{
    private void Awake()
    {        
        data.type = AmmoType.FMG_9x18;
        data.BaseDamage = 10f;
        data.attackModifier = new RangedAttackModifier();
    }
}
