using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneakers : ArmorItem
{
    protected override void Awake()
    {
        base.Awake();
        Name = "����";
        prefab = prefabsController.sneakers;
    }
}
