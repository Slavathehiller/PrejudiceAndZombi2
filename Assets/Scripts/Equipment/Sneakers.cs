using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneakers : ArmorItem
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Кеды";
        prefab = prefabsController.sneakers;
        Description = "Обычные дешевые кеды. Могут защитить от стеклянных осколков и укуса чихуахуа.";
    }
}
