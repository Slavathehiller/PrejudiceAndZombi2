using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackResult
{
    public bool Success;
    public float DamageAmount;
    public Vector3 AttackPoint;

    public AttackResult() { }
    public AttackResult(Vector3 point) 
    {
        AttackPoint = point;
    }


}

