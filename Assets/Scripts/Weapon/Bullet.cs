using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float initial_speed;
    private float speed;
    public MissleProperty property = new MissleProperty();
    private void Awake()
    {
        
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed, Space.Self);
    }

    public void Shoot(Vector3 point, RangedAttackModifier weaponModifier, AmmoData ammo, BaseEntity target)
    {
        property.weaponModifier = weaponModifier;
        property.ammo = ammo;
        property.target = target;
        transform.LookAt(point);
        speed = initial_speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
