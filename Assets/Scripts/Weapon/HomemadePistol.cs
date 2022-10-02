using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapon {
    public class HomemadePistol : RangedWeapon
    {
        public override string Caliber => "9x18 мм";

        protected override void Awake()
        {
            base.Awake();
            Name = "Самодельный пистолет";
            type = WeaponType.Pistol;
            ShootCost = 4;
            prefab = prefabsController.homemade_pistol;
            Description = "Пистолет, сделаный из подручных материалов";
        }

        protected override void Update()
        {
            base.Update();
        }
          

    }
}
