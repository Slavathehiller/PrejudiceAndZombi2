using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Weapon
{
    public class KitchenKnife : BaseWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            Name = "Кухонный нож";
            MeleeAttackModifier.damage = 10;
            type = WeaponType.Knife;
            prefab = prefabsController.kitchenKnife;
        }
    }
}
