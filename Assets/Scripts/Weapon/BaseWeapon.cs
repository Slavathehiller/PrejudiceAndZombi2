using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public enum WeaponType
    {
        None = 0,
        Knife = 1
    }
    public abstract class BaseWeapon : TacticalItem
    {

        [HideInInspector]
        public MeleeAttackModifier MeleeAttackModifier = new MeleeAttackModifier();
        [HideInInspector]
        public WeaponType type;
    }    
}