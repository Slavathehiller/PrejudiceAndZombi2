using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapon {
    public class HomemadePistol : RangedWeapon
    {
        public HomemadePistol()
        {
            type = WeaponType.Pistol;
            ShootCost = 0;
            //cartridge = new HMPistolCartridge();
        }
    }
}
