using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapon {
    public class HomemadePistol : RangedWeapon
    {
        public override string Caliber => "9x18 ��";

        protected override void Awake()
        {
            base.Awake();
            Name = "����������� ��������";
            type = WeaponType.Pistol;
            ShootCost = 4;
            prefab = prefabsController.homemade_pistol;
            Description = "��������, �������� �� ��������� ����������";
        }

        protected override void Update()
        {
            base.Update();
        }
          

    }
}
