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

        [HideInInspector]
        public MeleeAttackModifier MeleeAttackModifier = new MeleeAttackModifier();

        public WeaponType type;
    }    
}