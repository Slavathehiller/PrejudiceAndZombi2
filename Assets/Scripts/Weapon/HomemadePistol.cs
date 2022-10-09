using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapon {
    public class HomemadePistol : RangedWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            Name = "—амодельный пистолет";
            type = WeaponType.Pistol;
            ShootCost = 4;
            prefab = prefabsController.homemade_pistol;
            Description = "ѕистолет, сделаный из подручных материалов. ћожно попасть в ростовую фигуру с п€ти метров.";
        }

        protected override void Update()
        {
            base.Update();
        }
          

    }
}
