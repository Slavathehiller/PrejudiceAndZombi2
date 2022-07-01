using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBelt : EquipmentItem
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Простой ремень";
    }
}
