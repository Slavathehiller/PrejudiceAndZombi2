using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Item
{
    protected override void Awake()
    {
        base.Awake();
        Name = "���������";
        prefab = prefabsController.wood;
    }
}

