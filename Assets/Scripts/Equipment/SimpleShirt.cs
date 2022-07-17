using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShirt : InventoryEquipmentItem
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Простая рубашка";
        prefab = prefabsController.simpleShirt;
    }
}
