using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBelt : InventoryEquipmentItem
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Простой ремень";
    }
}
