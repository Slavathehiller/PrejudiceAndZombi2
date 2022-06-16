using Assets.Scripts.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public class HomemadeSMG : RangedWeapon
    {
        public HomemadeSMG()
        {
            Name = "Самодельный пистолет-пулемёт";
            type = WeaponType.SMG;
            ShootCost = 4;
        }
    }
}
