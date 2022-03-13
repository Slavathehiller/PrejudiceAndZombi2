using Assets.Scripts.Entity;
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


        public ThingReference thingRef;

        public Sprite image;
        public void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                var character = other.gameObject.GetComponent<Character>();
                var nearObjects = character.pcontroller.nearObjects;
                nearObjects.AddThing(this, character);

            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                var nearObjects = other.gameObject.GetComponent<Character>().pcontroller.nearObjects;
                nearObjects.DeleteThing(thingRef);
            }
        }

    }
    

}