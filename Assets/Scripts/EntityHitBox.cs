using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHitBox : MonoBehaviour
{
    public BaseEntity entity;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Missle")
        {
            var bullet = other.gameObject.GetComponent<Bullet>();
            var damage = bullet.property.ammo.BaseDamage;
            var critChance = bullet.property.ammo.attackModifier.CritModifier;
            if (bullet.property.target is BaseEntity && entity == (BaseEntity)bullet.property.target)  //≈сли попали куда целились, добавл€ем к шансу криртикала модификатор от оружи€
                critChance += bullet.property.weaponModifier.CritModifier;
            if(Random.Range(0f, 100f) <= critChance)
            {
                damage *= 2;
                Debug.Log(" ритический урон!");
            }
            entity.TakeDamage(damage);
            Debug.Log(entity.Name + " получает " + damage + " урона");
            entity.econtroller.animator.SetTrigger("Hit");
            Destroy(other.gameObject, 1);
        }
    }
}
