using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public enum WeaponType
    {
        None = 0,
        Knife = 1
    }
    public abstract class BaseWeapon : MonoBehaviour
    {

        [HideInInspector]
        public MeleeAttackModifier MeleeAttackModifier = new MeleeAttackModifier();
        [HideInInspector]
        public WeaponType type;

        public Sprite image;
    }
}