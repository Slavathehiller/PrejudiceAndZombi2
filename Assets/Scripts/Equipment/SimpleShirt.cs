using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShirt : EquipmentItem
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Простая рубашка";
    }
}
