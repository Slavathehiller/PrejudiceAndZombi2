using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapon {
    public class HomemadePistol : RangedWeapon
    {
        public HomemadePistol()
        {
            Name = "����������� ��������";
            type = WeaponType.Pistol;
            ShootCost = 4;
        }
    }
}
