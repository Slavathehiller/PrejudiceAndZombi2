using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public enum WeaponType
    {
        None = 0,
        Knife = 1,
        Pistol = 2,
        SMG = 3
    }
    public abstract class BaseWeapon : TacticalItem
    {
        public override string StatsInfo
        {
            get
            {
                string result = "";
                if (MeleeAttackModifier.damage > 0)
                    result = $"+ {MeleeAttackModifier.damage} к урону в ближнем бою \n";
                return result;
            }
        }

        [HideInInspector]
        public MeleeAttackModifier MeleeAttackModifier = new MeleeAttackModifier();

        public WeaponType type;
    }

    
}