using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Weapon
{
    public class KitchenKnife : BaseWeapon
    {

        public KitchenKnife()
        {
            MeleeAttackModifier.damage = 10;
            type = WeaponType.Knife;
        }
    }
}
