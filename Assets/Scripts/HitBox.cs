using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public BaseEntity entity;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Missle")
        {
            var bullet = other.gameObject.GetComponent<Bullet>();
            var damage = bullet.property.BaseDamage;
            if(Random.Range(0f, 100f) <= bullet.attackResult.criticalChance)
            {
                damage *= 2;
                Debug.Log("Критический урон!");
            }
            entity.TakeDamage(damage);
            Debug.Log(entity.Name + " получает " + damage + " урона");
            entity.econtroller.animator.SetTrigger("Hit");
            Destroy(other.gameObject, 1);
        }
    }
}
