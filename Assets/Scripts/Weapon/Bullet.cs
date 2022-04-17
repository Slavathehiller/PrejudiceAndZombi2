using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public MissleProperty property = new MissleProperty();
    public RangedAttackResult attackResult;

    private void Awake()
    {
        property.BaseDamage = 20f;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
