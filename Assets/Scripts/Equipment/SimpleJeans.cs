using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleJeans : InventoryEquipmentItem
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Простые джинсы";
        prefab = prefabsController.simpleJeans;
    }
}
