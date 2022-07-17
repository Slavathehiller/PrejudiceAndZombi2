using Assets.Scripts.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public class HomemadeSMG : RangedWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            Name = "Самодельный пистолет-пулемёт";
            type = WeaponType.SMG;
            ShootCost = 4;
            prefab = prefabsController.homemade_SMG;
        }
    }
}
