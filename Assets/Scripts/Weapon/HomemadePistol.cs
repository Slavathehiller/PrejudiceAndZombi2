using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapon {
    public class HomemadePistol : RangedWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            Name = "Самодельный пистолет";
            type = WeaponType.Pistol;
            ShootCost = 4;
            prefab = prefabsController.homemade_pistol;
        }

        protected override void Update()
        {
            base.Update();
        }
          

    }
}
