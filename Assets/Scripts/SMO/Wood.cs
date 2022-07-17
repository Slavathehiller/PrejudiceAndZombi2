using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Item
{
    protected override void Awake()
    {
        base.Awake();
        Name = "Древесина";
        prefab = prefabsController.wood;
    }
}

