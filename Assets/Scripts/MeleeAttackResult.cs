using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackResult
{
    public bool Success;
    public float DamageAmount;
    public Vector3 AttackPoint;

    public MeleeAttackResult() { }
    public MeleeAttackResult(Vector3 point) 
    {
        AttackPoint = point;
    }


}

